namespace QualEaResposta.Application.Dtos.Alternativa
{
    /// <summary>
    /// DTO para exibição de uma alternativa associada a uma pergunta.
    /// </summary>
    public class ViewAlternativaDto
    {
        /// <summary>
        /// Identificador único da alternativa.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Texto da alternativa.
        /// </summary>
        /// <example>Brasil</example>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string TextoAlternativa { get; set; } = string.Empty; // Inicialização para evitar null

        /// <summary>
        /// Indica se esta alternativa é a resposta correta.
        /// </summary>
        /// <example>true</example>
        public bool Correta { get; set; }

        /// <summary>
        /// Status atual da alternativa.
        /// </summary>
        /// <example>Ativo</example>
        public EStatus Status { get; set; } = EStatus.Ativo; // Inicialização com valor padrão

        /// <summary>
        /// Data de criação da alternativa.
        /// </summary>
        /// <example>2024-08-25T15:30:00</example>
        public DateTime CriadoEm { get; set; } = DateTime.Now; // Inicialização com data e hora atuais
    }
}