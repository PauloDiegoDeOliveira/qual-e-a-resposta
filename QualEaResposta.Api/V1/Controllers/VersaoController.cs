//namespace QualEaResposta.Api.V1.Controllers
//{
//    [ApiVersion("1.0")]
//    [Route("/v{version:apiVersion}/versao")]
//    [ApiController]
//    public class VersaoController(IWebHostEnvironment environment,
//                                  INotifier notifier,
//                                  IUser user) : ControladorBase(notifier, user)
//    {
//        private readonly IWebHostEnvironment environment = environment;

//        /// <summary>
//        /// Informa a versão da API e o ambiente.
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public IActionResult Valor(ApiVersion version)
//        {
//            var versionInfo = new
//            {
//                ApiVersion = version.ToString(),
//                Environment = environment.EnvironmentName,
//                Timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
//            };

//            if (IsValidOperation())
//            {
//                NotifyWarning("Versão encontrada.");
//            }

//            return CustomResponse(versionInfo);
//        }
//    }
//}