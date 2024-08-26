namespace QualEaResposta.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/versao")]
    [ApiController]
    public class VersaoController(IWebHostEnvironment environment,
                                  INotificador notifier,
                                  IUser user) : MainController(notifier, user)
    {
        private readonly IWebHostEnvironment environment = environment;

        /// <summary>
        /// Informa a versão da API e o ambiente.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Valor(ApiVersion version)
        {
            if (version == null)
            {
                NotificarErro("A versão da API não foi fornecida corretamente.");
                return CustomResponse();
            }

            var versionInfo = new
            {
                ApiVersion = version.ToString(),
                Environment = environment.EnvironmentName,
                Timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                User = AppUser.GetUserEmail(),
            };

            if (OperacaoValida())
            {
                NotificarMensagem("Versão da API retornada com sucesso.");
                return CustomResponse(versionInfo);
            }

            return CustomResponse();
        }
    }
}