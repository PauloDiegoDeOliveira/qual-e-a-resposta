namespace QualEaResposta.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private const string SUCESSO = "sucesso";
        private const string MENSAGEM = "mensagem";
        private const string DADOS = "dados";

        // Campos protegidos para uso em classes derivadas
        protected readonly INotificationService _notificationService;

        protected readonly IUser _appUser;
        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

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

        // Método para verificar se a operação é válida (sem notificações de erro)
        protected bool OperacaoValida()
        {
            return _notificationService.OperacaoValida();
        }

        // Método de resposta padronizado para sucesso ou falha
        protected ActionResult ResponderPadronizado(object? result = null)
        {
            // Verificação explícita de nulidade
            result ??= new { }; // ou qualquer valor padrão desejado

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

        // Método de resposta padronizado usando ModelState
        protected ActionResult ResponderPadronizado(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotificarErroModelInvalida(modelState);
            }

            return ResponderPadronizado();
        }

        // Método para notificar erros de validação de modelo
        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(v => v.Errors);

            foreach (ModelError erro in erros)
            {
                string? errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                _notificationService.NotificarErro(errorMessage);
            }
        }

        // Método para notificar um erro específico
        protected void NotificarErro(string mensagem)
        {
            _notificationService.NotificarErro(mensagem);
        }

        // Método para notificar uma mensagem de sucesso
        protected void NotificarMensagem(string mensagem)
        {
            _notificationService.NotificarMensagem(mensagem);
        }
    }
}