namespace QualEaResposta.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VersaoController(IWebHostEnvironment environment,
                                  INotificationService notificationService,
                                  IUser user) : MainController(notificationService, user)
    {
        private readonly IWebHostEnvironment _environment = environment;

        /// <summary>
        /// Informa a versão da API e o ambiente.
        /// </summary>
        /// <returns>Informações sobre a versão da API e o ambiente atual</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ViewApiVersionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Valor(ApiVersion version)
        {
            if (version == null)
            {
                NotificarErro("A versão da API não foi fornecida corretamente.");
                return ResponderPadronizado();
            }

            var versionInfo = new ViewApiVersionDto
            {
                ApiVersion = version.ToString(),
                Environment = _environment.EnvironmentName,
                Timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                User = _appUser.GetUserEmail(),
            };

            if (OperacaoValida())
            {
                NotificarMensagem("Versão da API retornada com sucesso.");
                return ResponderPadronizado(versionInfo);
            }

            return ResponderPadronizado();
        }
    }
}