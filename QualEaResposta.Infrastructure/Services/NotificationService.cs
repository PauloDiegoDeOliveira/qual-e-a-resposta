namespace QualEaResposta.Infrastructure.Services
{
    public class NotificationService(INotificador notificador) : INotificationService, INotificador
    {
        private readonly INotificador _notificador = notificador ?? new DefaultNotificador();

        public void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        public void NotificarMensagem(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem, ETipoNotificacao.Mensagem));
        }

        public bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        public List<string> ObterMensagens()
        {
            return _notificador.ObterNotificacoes().Select(n => n.Mensagem).ToList();
        }

        // Implementação de métodos da interface INotificador
        public bool TemNotificacao()
        {
            return _notificador.TemNotificacao();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificador.ObterNotificacoes();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificador.Handle(notificacao);
        }
    }

    // Classe de notificador padrão para evitar erros de null
    public class DefaultNotificador : INotificador
    {
        private readonly List<Notificacao> _notificacoes = [];

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
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