namespace QualEaResposta.Infrastructure.Services
{
    /// <summary>
    /// Serviço de notificação que gerencia e manipula notificações de erro e mensagens de sucesso.
    /// </summary>
    public class NotificationService : INotificationService, INotificador
    {
        private readonly List<Notificacao> _notificacoesErro = new(); // Lista para notificações de erro
        private readonly List<Notificacao> _notificacoesMensagem = new(); // Lista para notificações de mensagens de sucesso

        public void NotificarErro(string mensagem)
        {
            HandleErro(new Notificacao(mensagem));
        }

        public void NotificarMensagem(string mensagem)
        {
            HandleMensagem(new Notificacao(mensagem, ETipoNotificacao.Mensagem));
        }

        public bool OperacaoValida()
        {
            // Só considera a operação inválida se houver notificações de erro
            return _notificacoesErro.Count == 0;
        }

        public List<string> ObterMensagens()
        {
            // Retorna tanto as mensagens de erro quanto de sucesso
            return _notificacoesErro.Concat(_notificacoesMensagem)
                                    .Select(n => n.Mensagem)
                                    .ToList();
        }

        // Implementação de métodos da interface INotificador

        public bool TemNotificacao()
        {
            return _notificacoesErro.Count != 0 || _notificacoesMensagem.Count != 0;
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoesErro.Concat(_notificacoesMensagem).ToList();
        }

        private void HandleErro(Notificacao notificacao)
        {
            _notificacoesErro.Add(notificacao);
        }

        private void HandleMensagem(Notificacao notificacao)
        {
            _notificacoesMensagem.Add(notificacao);
        }

        public void Handle(Notificacao notificacao)
        {
            if (notificacao.Tipo == ETipoNotificacao.Erro)
                HandleErro(notificacao);
            else
                HandleMensagem(notificacao);
        }
    }
}