namespace QualEaResposta.Infrastructure.Services
{
    /// <summary>
    /// Serviço de notificação que gerencia e manipula notificações de erro e mensagens de sucesso.
    /// </summary>
    public class NotificationService : INotificationService, INotificador
    {
        private readonly List<Notificacao> _notificacoes = [];

        public void NotificarErro(string mensagem)
        {
            Handle(new Notificacao(mensagem));
        }

        public void NotificarMensagem(string mensagem)
        {
            Handle(new Notificacao(mensagem, ETipoNotificacao.Mensagem));
        }

        public bool OperacaoValida()
        {
            return _notificacoes.Count == 0;
        }

        public List<string> ObterMensagens()
        {
            return _notificacoes.Select(n => n.Mensagem).ToList();
        }

        // Implementação de métodos da interface INotificador

        public bool TemNotificacao()
        {
            return _notificacoes.Count != 0;
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
    }
}