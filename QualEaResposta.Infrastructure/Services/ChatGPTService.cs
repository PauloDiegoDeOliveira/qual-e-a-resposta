namespace QualEaResposta.Infrastructure.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly OpenAIConfig _config;

        public ChatGPTService(HttpClient httpClient, IOptions<OpenAIConfig> options)
        {
            _config = options.Value;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.AuthSecret);
            _httpClient.BaseAddress = new Uri(_config.BaseAddress);

            // Ajusta a política de retry para tentar menos vezes com mais tempo de espera exponencial
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // Espera exponencial entre tentativas
        }

        public async Task<string> GetCorrectAnswerAsync(string question, List<string?> alternatives)
        {
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

            OpenAIRequest? request = new(prompt);
            string? json = JsonSerializer.Serialize(request);
            StringContent? content = new(json, Encoding.UTF8, "application/json");

            // Usa a política de retry para fazer a chamada da API
            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync("completions", content));

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument jsonDoc = JsonDocument.Parse(jsonResponse);

                // Verifica a estrutura JSON antes de acessar
                if (jsonDoc.RootElement.TryGetProperty("choices", out JsonElement choices) &&
                    choices.GetArrayLength() > 0 &&
                    choices[0].TryGetProperty("message", out JsonElement message) &&
                    message.TryGetProperty("content", out JsonElement contentElement))
                {
                    return contentElement.GetString() ?? string.Empty;
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Lida especificamente com o erro 401
                throw new Exception("Erro de autenticação: verifique sua chave de API.");
            }

            // Retorna uma string vazia se algo deu errado ou se não encontrou a resposta correta
            return string.Empty;
        }
    }
}