namespace QualEaResposta.Domain.Services
{
    public class PerguntaDomainService
    {
        public bool ValidarPergunta(Pergunta pergunta)
        {
            return !string.IsNullOrWhiteSpace(pergunta.TextoPergunta) && pergunta.Alternativas.Count > 0;
        }
    }
}