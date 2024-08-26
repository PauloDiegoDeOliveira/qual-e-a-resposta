namespace QualEaResposta.Domain.Model
{
    /// <summary>
    /// Representa uma pergunta no sistema.
    /// </summary>
    public class Pergunta : EntidadeBase
    {
        /// <summary>
        /// Texto da pergunta.
        /// </summary>
        public string TextoPergunta { get; set; } = string.Empty;

        /// <summary>
        /// Coleção de alternativas associadas a esta pergunta.
        /// </summary>
        public ICollection<Alternativa> Alternativas { get; set; } = []; // Inicialização adequada para evitar null
    }
}