namespace QualEaResposta.Api.V1.Controllers
{
    /// <summary>
    /// Controlador para fornecer informações sobre a versão da API e o ambiente de execução.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="VersaoController"/>.
    /// </remarks>
    /// <param name="environment">O ambiente de hospedagem da aplicação.</param>
    /// <param name="notificationService">O serviço de notificação para gerenciar mensagens de erro e sucesso.</param>
    /// <param name="user">O serviço de usuário para gerenciar informações do usuário autenticado.</param>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VersaoController(IWebHostEnvironment environment,
                                  INotificationService notificationService,
                                  IUser user) : MainController(notificationService, user)
    {
        private readonly IWebHostEnvironment _environment = environment;

        /// <summary>
        /// Informa a versão da API e o ambiente de execução atual.
        /// </summary>
        /// <param name="version">A versão da API solicitada.</param>
        /// <returns>Um <see cref="IActionResult"/> contendo informações sobre a versão da API e o ambiente de execução atual.</returns>
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