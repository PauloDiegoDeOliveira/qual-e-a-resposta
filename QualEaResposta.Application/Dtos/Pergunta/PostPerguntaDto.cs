namespace QualEaResposta.Application.Dtos.Pergunta
{
    /// <summary>
    /// DTO para criação de uma nova pergunta no quiz.
    /// </summary>
    public class PostPerguntaDto
    {
        /// <summary>
        /// Texto da pergunta para o quiz.
        /// Exemplo de pergunta: "Em qual país foi inventado o chuveiro elétrico?"
        /// </summary>
        /// <example>Em qual país foi inventado o chuveiro elétrico?</example>
        [Display(Name = "pergunta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string TextoPergunta { get; set; } = string.Empty; // Inicialização para evitar null

        /// <summary>
        /// Lista de alternativas para a pergunta fornecida.
        /// Cada alternativa inclui um texto e uma indicação se é a resposta correta.
        /// A lista deve conter pelo menos uma alternativa correta.
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// [
        ///     { "TextoAlternativa": "Brasil" },
        ///     { "TextoAlternativa": "Estados Unidos" },
        ///     { "TextoAlternativa": "França" },
        ///     { "TextoAlternativa": "Alemanha" },
        ///     { "TextoAlternativa": "Japão" }
        /// ]
        /// ]]>
        /// </example>
        [Display(Name = "alternativas")]
        [Required(ErrorMessage = "É necessário fornecer pelo menos uma alternativa.")]
        [MinLength(1, ErrorMessage = "É necessário fornecer pelo menos uma alternativa.")]
        public List<PostAlternativaDto> Alternativas { get; set; } = []; // Inicialização com uma nova lista para evitar null
    }
}