namespace QualEaResposta.Domain.Model
{
    /// <summary>
    /// Representa uma alternativa para uma pergunta.
    /// </summary>
    public class Alternativa : EntidadeBase
    {
        /// <summary>
        /// Texto que representa a alternativa de uma pergunta.
        /// </summary>
        public string TextoAlternativa { get; set; } = string.Empty;

        /// <summary>
        /// Indica se esta alternativa é a correta para a pergunta associada.
        /// </summary>
        public bool ECorreta { get; set; }

        /// <summary>
        /// Identificador da pergunta associada a esta alternativa.
        /// </summary>
        public Guid PerguntaId { get; set; }

        /// <summary>
        /// Pergunta associada a esta alternativa.
        /// </summary>
        public Pergunta Pergunta { get; set; } = new Pergunta();
    }
}