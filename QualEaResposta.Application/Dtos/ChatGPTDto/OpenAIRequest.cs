namespace QualEaResposta.Application.Dtos.ChatGPTDto
{
    /// <summary>
    /// Representa uma solicitação para o modelo OpenAI.
    /// </summary>
    public class OpenAIRequest
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="OpenAIRequest"/>.
        /// </summary>
        /// <param name="question">A pergunta a ser enviada ao modelo OpenAI.</param>
        public OpenAIRequest(string question)
        {
            Model = "gpt-4o-mini";
            Messages =
            [
                new { role = "user", content = question }
            ];
            Temperature = 0f;
            MaxTokens = 500;
        }

        /// <summary>
        /// Obtém ou define o modelo OpenAI a ser utilizado.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Obtém ou define as mensagens a serem enviadas ao modelo OpenAI.
        /// </summary>
        [JsonPropertyName("messages")]
        public object[] Messages { get; set; }

        /// <summary>
        /// Obtém ou define a temperatura para controle de criatividade das respostas do modelo.
        /// </summary>
        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        /// <summary>
        /// Obtém ou define o número máximo de tokens para a resposta do modelo.
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }
    }
}