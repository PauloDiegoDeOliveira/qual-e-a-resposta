namespace QualEaResposta.Infrastructure.Services
{
    /// <summary>
    /// Serviço de notificação que gerencia e manipula notificações de erro e mensagens de sucesso.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="NotificationService"/>.
    /// </remarks>
    /// <param name="logger">Logger para registrar eventos e erros.</param>
    public class NotificationService(ILogger<NotificationService> logger) : INotificationService, INotificador
    {
        private readonly List<Notificacao> _notificacoesErro = []; // Lista para notificações de erro
        private readonly List<Notificacao> _notificacoesMensagem = []; // Lista para notificações de mensagens de sucesso
        private readonly ILogger<NotificationService> _logger = logger;

        public void NotificarErro(string mensagem)
        {
            _logger.LogInformation("Notificando erro: {Mensagem}", mensagem);
            HandleErro(new Notificacao(mensagem));
        }

        public void NotificarMensagem(string mensagem)
        {
            _logger.LogInformation("Notificando mensagem de sucesso: {Mensagem}", mensagem);
            HandleMensagem(new Notificacao(mensagem, ETipoNotificacao.Mensagem));
        }

        public bool OperacaoValida()
        {
            bool operacaoValida = _notificacoesErro.Count == 0;
            _logger.LogInformation("Verificação de operação válida: {OperacaoValida}", operacaoValida);
            return operacaoValida;
        }

        public List<string> ObterMensagens()
        {
            var mensagens = _notificacoesErro.Concat(_notificacoesMensagem)
                                             .Select(n => n.Mensagem)
                                             .ToList();
            _logger.LogInformation("Obtendo todas as mensagens. Total: {TotalMensagens}", mensagens.Count);
            return mensagens;
        }

        // Implementação de métodos da interface INotificador

        public bool TemNotificacao()
        {
            bool temNotificacao = _notificacoesErro.Count != 0 || _notificacoesMensagem.Count != 0;
            _logger.LogInformation("Verificação de notificações existentes: {TemNotificacao}", temNotificacao);
            return temNotificacao;
        }

        public List<Notificacao> ObterNotificacoes()
        {
            var notificacoes = _notificacoesErro.Concat(_notificacoesMensagem).ToList();
            _logger.LogInformation("Obtendo todas as notificações. Total: {TotalNotificacoes}", notificacoes.Count);
            return notificacoes;
        }

        private void HandleErro(Notificacao notificacao)
        {
            _logger.LogWarning("Adicionando notificação de erro: {Mensagem}", notificacao.Mensagem);
            _notificacoesErro.Add(notificacao);
        }

        private void HandleMensagem(Notificacao notificacao)
        {
            _logger.LogInformation("Adicionando notificação de mensagem: {Mensagem}", notificacao.Mensagem);
            _notificacoesMensagem.Add(notificacao);
        }

        public void Handle(Notificacao notificacao)
        {
            if (notificacao.Tipo == ETipoNotificacao.Erro)
            {
                _logger.LogWarning("Manipulando notificação de erro: {Mensagem}", notificacao.Mensagem);
                HandleErro(notificacao);
            }
            else
            {
                _logger.LogInformation("Manipulando notificação de mensagem: {Mensagem}", notificacao.Mensagem);
                HandleMensagem(notificacao);
            }
        }
    }
}