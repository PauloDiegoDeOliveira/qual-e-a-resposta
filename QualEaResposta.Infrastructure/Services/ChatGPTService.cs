namespace QualEaResposta.Infrastructure.Services
{
    /// <summary>
    /// Serviço que interage com a API do ChatGPT para obter respostas.
    /// </summary>
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly OpenAIConfig _config;
        private readonly ILogger<ChatGPTService> _logger;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ChatGPTService"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para fazer requisições.</param>
        /// <param name="options">Configurações da API OpenAI.</param>
        /// <param name="logger">Logger para registrar eventos e erros.</param>
        /// <param name="notificationService">Serviço de notificação para mensagens e erros.</param>
        public ChatGPTService(HttpClient httpClient, IOptions<OpenAIConfig> options, ILogger<ChatGPTService> logger, INotificationService notificationService)
        {
            _config = options.Value;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.AuthSecret);
            _httpClient.BaseAddress = new Uri(_config.BaseAddress);
            _logger = logger;
            _notificationService = notificationService;

            // Ajusta a política de retry para tentar menos vezes com mais tempo de espera exponencial
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // Espera exponencial entre tentativas
        }

        /// <summary>
        /// Obtém a resposta correta da API do ChatGPT com base na pergunta e alternativas fornecidas.
        /// </summary>
        /// <param name="question">Pergunta a ser enviada para o ChatGPT.</param>
        /// <param name="alternatives">Lista de alternativas para a pergunta.</param>
        /// <returns>Resposta obtida da API do ChatGPT.</returns>
        public async Task<string> GetCorrectAnswerAsync(string question, List<string?> alternatives)
        {
            _logger.LogInformation("Construindo o prompt para a pergunta.");

            string prompt = $"Pergunta: {question}\n";

            if (alternatives.Any(a => !string.IsNullOrEmpty(a)))
            {
                prompt += "Alternativas:\n";
                for (int i = 0; i < alternatives.Count; i++)
                {
                    if (!string.IsNullOrEmpty(alternatives[i]))
                    {
                        prompt += $"{(char)('A' + i)}: {alternatives[i]}\n";
                    }
                }
                prompt += "Qual é a resposta correta?";
            }
            else
            {
                prompt += "Qual é a resposta correta?";
            }

            _logger.LogInformation("Prompt construído: {Prompt}", prompt);

            OpenAIRequest request = new(prompt);
            string json = JsonSerializer.Serialize(request);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            _logger.LogInformation("Enviando solicitação para OpenAI API.");

            try
            {
                // Usa a política de retry para fazer a chamada da API
                HttpResponseMessage response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync("completions", content));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    using JsonDocument jsonDoc = JsonDocument.Parse(jsonResponse);

                    _logger.LogInformation("Resposta da OpenAI API recebida com sucesso.");

                    // Verifica a estrutura JSON antes de acessar
                    if (jsonDoc.RootElement.TryGetProperty("choices", out JsonElement choices) &&
                        choices.GetArrayLength() > 0 &&
                        choices[0].TryGetProperty("message", out JsonElement message) &&
                        message.TryGetProperty("content", out JsonElement contentElement))
                    {
                        string result = contentElement.GetString() ?? string.Empty;
                        _logger.LogInformation("Resposta processada com sucesso: {Resposta}", result);
                        _notificationService.NotificarMensagem("Resposta obtida com sucesso.");
                        return result;
                    }
                    else
                    {
                        _logger.LogWarning("A resposta da API não contém o campo esperado 'choices'. Resposta completa: {JsonResponse}", jsonResponse);
                        _notificationService.NotificarErro("A resposta da API não contém o campo esperado 'choices'.");
                        return string.Empty;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _logger.LogError("Erro de autenticação ao chamar a API da OpenAI.");
                    _notificationService.NotificarErro("Erro de autenticação: verifique sua chave de API.");
                    return string.Empty;
                }
                else
                {
                    _logger.LogWarning("Falha ao chamar a API da OpenAI. StatusCode: {StatusCode}, Reason: {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
                    _notificationService.NotificarErro($"Erro: {response.ReasonPhrase}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar obter a resposta da OpenAI API.");
                _notificationService.NotificarErro("Erro ao processar a solicitação. Tente novamente mais tarde.");
                return string.Empty;
            }
        }
    }
}