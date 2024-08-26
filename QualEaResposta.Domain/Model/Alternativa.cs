namespace QualEaResposta.Domain.Model
{
    public class Alternativa : EntidadeBase
    {
        public string TextoAlternativa { get; set; } = string.Empty;
        public bool ECorreta { get; set; }

        public Guid PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; } = new Pergunta();
    }
}