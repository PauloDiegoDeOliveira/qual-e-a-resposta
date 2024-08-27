namespace QualEaResposta.Application.Dtos.Alternativa
{
    /// <summary>
    /// DTO para envio de uma alternativa associada a uma pergunta.
    /// </summary>
    public class PostAlternativaDto
    {
        /// <summary>
        /// Texto da alternativa para a pergunta "Em qual país foi inventado o chuveiro elétrico?"
        /// </summary>
        /// <example>Brasil</example>
        [JsonPropertyName("alternativa")]
        [Display(Name = "alternativa")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string TextoAlternativa { get; set; } = string.Empty; // Inicialização para evitar null

        /// <summary>
        /// Indica se esta alternativa é a resposta correta.
        /// Por exemplo, "true" se a alternativa "Brasil" for a resposta correta para a pergunta "Em qual país foi inventado o chuveiro elétrico?".
        /// </summary>
        /// <example>true</example>
        [JsonPropertyName("correta")]
        [Display(Name = "correta")]
        public bool Correta { get; set; } // Incluído se necessário durante a criação
    }
}