namespace QualEaResposta.Application.DTOs
{
    /// <summary>
    /// Representa as configurações necessárias para conectar-se à API da OpenAI.
    /// </summary>
    public class OpenAIConfig
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="OpenAIConfig"/>.
        /// Construtor sem parâmetros necessário para a injeção de dependência.
        /// </summary>
        public OpenAIConfig()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="OpenAIConfig"/> com parâmetros.
        /// </summary>
        /// <param name="baseAddress">O endereço base da API OpenAI.</param>
        /// <param name="authSecret">O segredo de autenticação (chave de API) para a API OpenAI.</param>
        public OpenAIConfig(string baseAddress, string authSecret)
        {
            BaseAddress = baseAddress;
            AuthSecret = authSecret;
        }

        /// <summary>
        /// Obtém ou define o endereço base da API OpenAI.
        /// </summary>
        public string BaseAddress { get; set; } = string.Empty; // Inicialização padrão para evitar nulos

        /// <summary>
        /// Obtém ou define o segredo de autenticação (chave de API) para a API OpenAI.
        /// </summary>
        public string AuthSecret { get; set; } = string.Empty; // Inicialização padrão para evitar nulos
    }
}