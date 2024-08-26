namespace QualEaResposta.Domain.Model
{
    public class Pergunta : EntidadeBase
    {
        public string TextoPergunta { get; set; } = string.Empty;
        public ICollection<Alternativa> Alternativas { get; set; } = [];
    }
}