namespace QualEaResposta.Api.V2.Controllers
{
    /// <summary>
    /// Controlador para fornecer informações sobre a versão da API e o ambiente de execução para a versão 2.0 da API.
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VersaoController : MainController
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="VersaoController"/>.
        /// </summary>
        /// <param name="environment">O ambiente de hospedagem da aplicação.</param>
        /// <param name="notificationService">O serviço de notificação para gerenciar mensagens de erro e sucesso.</param>
        /// <param name="user">O serviço de usuário para gerenciar informações do usuário autenticado.</param>
        public VersaoController(IWebHostEnvironment environment,
                                INotificationService notificationService,
                                IUser user) : base(notificationService, user)
        {
            _environment = environment;
        }

        /// <summary>
        /// Informa a versão da API e o ambiente de execução atual.
        /// </summary>
        /// <param name="version">A versão atual da API.</param>
        /// <returns>Um <see cref="IActionResult"/> contendo informações sobre a versão da API e o ambiente de execução atual.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ViewApiVersionDto), StatusCodes.Status200OK)] // Ajustado para ViewApiVersionDto
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Valor(ApiVersion version)
        {
            // Verificação se a versão foi fornecida corretamente
            if (version == null)
            {
                NotificarErro("A versão da API não foi fornecida corretamente.");
                return ResponderPadronizado();
            }

            // Coleta de informações sobre a versão da API e o ambiente
            var versionInfo = new ViewApiVersionDto
            {
                ApiVersion = version.ToString(),
                Environment = _environment.EnvironmentName,
                Timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                User = _appUser.GetUserEmail(),
            };

            // Verificação de operação válida
            if (OperacaoValida())
            {
                NotificarMensagem("Versão da API retornada com sucesso.");
                return ResponderPadronizado(versionInfo);
            }

            return ResponderPadronizado();
        }
    }
}