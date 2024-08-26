namespace QualEaResposta.Application.Dtos.ApiVersionDto
{
    /// <summary>
    /// DTO para exibição de informações sobre a versão da API e o ambiente atual.
    /// </summary>
    public class ViewApiVersionDto
    {
        /// <summary>
        /// Versão da API.
        /// </summary>
        /// <example>1.0</example>
        public string ApiVersion { get; set; } = string.Empty;

        /// <summary>
        /// Ambiente em que a API está sendo executada.
        /// </summary>
        /// <example>Produção</example>
        public string Environment { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora em que a informação foi gerada.
        /// </summary>
        /// <example>2024-08-25T15:30:00</example>
        public string Timestamp { get; set; } = string.Empty;

        /// <summary>
        /// Email do usuário que fez a solicitação.
        /// </summary>
        /// <example>usuario@exemplo.com</example>
        public string User { get; set; } = string.Empty;
    }
}