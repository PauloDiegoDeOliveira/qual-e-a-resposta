//using Microsoft.AspNetCore.Hosting;

//namespace QualEaResposta.Api.V2.Controllers
//{
//    [ApiVersion("2.0")]
//    [Route("api/v{version:apiVersion}/versao")]
//    [ApiController]
//    public class VersaoController(IWebHostEnvironment environment,
//                                    INotifier notifier,
//                                    IUser user) : MainController(notifier, user)
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
//                User = user.GetUserEmail(),
//                Role = user.GetUserRole()
//            };

//            if (IsValidOperation())
//            {
//                NotifyWarning("Versão encontrada.");
//            }

//            return CustomResponse(versionInfo);
//        }
//    }
//}