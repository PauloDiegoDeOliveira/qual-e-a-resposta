namespace QualEaResposta.Domain.Model
{
    public class EntidadeBase
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime? CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AlteradoEm { get; set; }
    }
}