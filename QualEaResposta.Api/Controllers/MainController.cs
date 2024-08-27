namespace QualEaResposta.Api.Controllers
{
    /// <summary>
    /// Controlador base que fornece métodos auxiliares para outros controladores.
    /// </summary>
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private const string SUCESSO = "sucesso";
        private const string MENSAGEM = "mensagem";
        private const string DADOS = "dados";

        /// <summary>
        /// Serviço de notificação para gerenciar mensagens e erros.
        /// </summary>
        protected readonly INotificationService _notificationService;

        /// <summary>
        /// Serviço de usuário para obter informações sobre o usuário autenticado.
        /// </summary>
        protected readonly IUser _appUser;

        /// <summary>
        /// Identificador do usuário autenticado.
        /// </summary>
        protected Guid UsuarioId { get; set; }

        /// <summary>
        /// Indica se o usuário está autenticado.
        /// </summary>
        protected bool UsuarioAutenticado { get; set; }

        /// <summary>
        /// Construtor para inicializar serviços de notificação e informações do usuário.
        /// </summary>
        /// <param name="notificationService">Serviço de notificação.</param>
        /// <param name="appUser">Serviço de usuário.</param>
        protected MainController(INotificationService notificationService, IUser appUser)
        {
            _notificationService = notificationService;
            _appUser = appUser;

            if (_appUser.IsAuthenticated())
            {
                UsuarioId = _appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        /// <summary>
        /// Verifica se a operação atual é válida, sem notificações de erro.
        /// </summary>
        /// <returns>Retorna true se não houver notificações de erro, caso contrário, false.</returns>
        protected bool OperacaoValida()
        {
            return _notificationService.OperacaoValida();
        }

        /// <summary>
        /// Gera uma resposta padronizada baseada no resultado da operação.
        /// </summary>
        /// <param name="result">Objeto de resultado a ser retornado.</param>
        /// <returns>Retorna um ActionResult padronizado.</returns>
        protected ActionResult ResponderPadronizado(object? result = null)
        {
            result ??= new { }; // Inicialização para evitar null

            if (OperacaoValida())
            {
                return Ok(new
                {
                    sucesso = true,
                    mensagem = _notificationService.ObterMensagens(),
                    dados = result
                });
            }

            return BadRequest(new
            {
                sucesso = false,
                mensagem = _notificationService.ObterMensagens(),
                dados = result
            });
        }

        /// <summary>
        /// Gera uma resposta padronizada baseada no estado do modelo.
        /// </summary>
        /// <param name="modelState">Estado do modelo a ser verificado.</param>
        /// <returns>Retorna um ActionResult padronizado.</returns>
        protected ActionResult ResponderPadronizado(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotificarErroModelInvalida(modelState);
            }

            return ResponderPadronizado();
        }

        /// <summary>
        /// Notifica erros baseados na validação do modelo.
        /// </summary>
        /// <param name="modelState">Estado do modelo contendo os erros de validação.</param>
        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(v => v.Errors);

            foreach (var erro in erros)
            {
                var errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                _notificationService.NotificarErro(errorMessage);
            }
        }

        /// <summary>
        /// Notifica um erro específico.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro a ser notificada.</param>
        protected void NotificarErro(string mensagem)
        {
            _notificationService.NotificarErro(mensagem);
        }

        /// <summary>
        /// Notifica uma mensagem de sucesso.
        /// </summary>
        /// <param name="mensagem">Mensagem de sucesso a ser notificada.</param>
        protected void NotificarMensagem(string mensagem)
        {
            _notificationService.NotificarMensagem(mensagem);
        }
    }
}