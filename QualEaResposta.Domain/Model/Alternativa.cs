namespace QualEaResposta.Domain.Model
{
    public class Alternativa : EntidadeBase
    {
        public string TextoAlternativa { get; set; }
        public bool EhCorreta { get; set; }

        public Guid PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; }
    }
}