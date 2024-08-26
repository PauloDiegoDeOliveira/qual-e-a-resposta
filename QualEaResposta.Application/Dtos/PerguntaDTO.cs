namespace QualEaResposta.Application.Dtos
{
    public class PerguntaDTO
    {
        public Guid Id { get; set; }
        public string TextoPergunta { get; set; }
        public List<AlternativaDTO> Alternativas { get; set; }
    }
}