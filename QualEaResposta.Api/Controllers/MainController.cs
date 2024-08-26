namespace QualEaResposta.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador notificador;

        protected readonly IUser AppUser;
        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected MainController(INotificador notificador, IUser appUser)
        {
            this.notificador = notificador;
            AppUser = appUser;

            if (AppUser.IsAuthenticated())
            {
                UsuarioId = AppUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected bool OperacaoValida()
        {
            return !notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    sucesso = true,
                    mensagem = notificador.ObterNotificacoes().Select(n => n.Mensagem).ToList(),
                    dados = result
                });
            }

            return BadRequest(new
            {
                sucesso = false,
                mensagem = notificador.ObterNotificacoes().Select(n => n.Mensagem).ToList(),
                dados = result
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);

            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values
                .SelectMany(obj => obj.Errors);

            foreach (var erro in erros)
            {
                var errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;

                NotificarErro(errorMessage);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            notificador.Handle(new Notificacao(mensagem));
        }

        protected void NotificarMensagem(string mensagem)
        {
            notificador.Handle(new Notificacao(mensagem, ETipoNotificacao.Mensagem));
        }
    }
}