using QualEaResposta.Domain.Enums;

namespace QualEaResposta.Domain.Model
{
    /// <summary>
    /// Classe base para entidades no sistema.
    /// </summary>
    public class EntidadeBase
    {
        /// <summary>
        /// Identificador único da entidade.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Status da entidade.
        /// </summary>
        public EStatus Status { get; set; } = EStatus.Ativo; // Usando o enum diretamente

        /// <summary>
        /// Data e hora de criação da entidade.
        /// </summary>
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // Usando UtcNow para consistência global

        /// <summary>
        /// Data e hora da última alteração na entidade.
        /// </summary>
        public DateTime? AlteradoEm { get; set; }
    }
}