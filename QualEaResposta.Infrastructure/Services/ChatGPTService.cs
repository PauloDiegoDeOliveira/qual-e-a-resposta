namespace QualEaResposta.Infrastructure.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey = "ChaveAqui";

        public ChatGPTService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
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

            var content = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 50
            };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", content);

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

            // Retorna uma string vazia se algo deu errado ou se não encontrou a resposta correta
            return string.Empty;
        }
    }
}