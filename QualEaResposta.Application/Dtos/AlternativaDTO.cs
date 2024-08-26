namespace QualEaResposta.Application.Dtos
{
    public class AlternativaDTO
    {
        public Guid Id { get; set; }
        public string TextoAlternativa { get; set; }
        public bool ECorreta { get; set; }
    }
}