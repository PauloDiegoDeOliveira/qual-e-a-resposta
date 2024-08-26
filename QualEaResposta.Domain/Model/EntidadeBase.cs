using QualEaResposta.Domain.Enums;

namespace QualEaResposta.Domain.Model
{
    public class EntidadeBase
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = EStatus.Ativo.ToString();
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AlteradoEm { get; set; }
    }
}