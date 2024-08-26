namespace QualEaResposta.Domain.Core.Interfaces.Services
{
    public interface INotificationService
    {
        void NotificarErro(string mensagem);

        void NotificarMensagem(string mensagem);

        bool OperacaoValida();

        List<string> ObterMensagens();
    }
}