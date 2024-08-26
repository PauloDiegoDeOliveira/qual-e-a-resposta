//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using System.Collections.Generic;
//using System.Text;

//namespace QualEaResposta.Api.Controllers
//{
//    [ApiController]
//    public abstract class MainController : ControllerBase
//    {
//        private readonly INotifier notifier;
//        public readonly IUser user;
//        protected Guid UsuarioId { get; set; }
//        protected bool UsuarioAutenticado { get; set; }
//        protected IEnumerable<string> UsuarioClaims { get; set; }

//        protected MainController(INotifier notifier,
//                                 IUser user)
//        {
//            this.notifier = notifier;
//            this.user = user;

//            if (user.IsAuthenticated())
//            {
//                UsuarioId = user.GetUserId();
//                UsuarioAutenticado = true;
//                UsuarioClaims = user.GetUserClaims();
//            }
//        }

//        protected bool IsValidOperation()
//        {
//            return !notifier.HasAnyError();
//        }

//        protected IActionResult CustomResponse(object result = null)
//        {
//            var responseBody = new
//            {
//                sucesso = IsValidOperation(),
//                mensagem = notifier.GetAllNotifications().Select(n => n.Message),
//                dados = result
//            };

//            return IsValidOperation() ? Ok(responseBody) : BadRequest(responseBody);
//        }

//        protected IActionResult CustomResponse(ModelStateDictionary modelState)
//        {
//            if (!modelState.IsValid)
//                NotifyInvalidModels(modelState);

//            return CustomResponse();
//        }

//        protected void NotifyInvalidModels(ModelStateDictionary modelState)
//        {
//            IEnumerable<ModelError> errors = modelState.Values.SelectMany(e => e.Errors);
//            foreach (ModelError error in errors)
//            {
//                string errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
//                NotifyError(errorMsg);
//            }
//        }

//        protected void NotifyError(string message)
//        {
//            notifier.AddNotification(new Notification(message));
//        }

//        protected void NotificarErro(List<string> messageList)
//        {
//            foreach (string erro in messageList)
//            {
//                NotifyError(erro);
//            }
//        }

//        protected void NotifyWarning(string message)
//        {
//            notifier.AddNotification(new Notification(message, ENotificationType.Warning));
//        }

//        protected string ExtrairTokenDoCabecalho(string authorizationHeader)
//        {
//            return authorizationHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) ?? false
//                ? authorizationHeader["Bearer ".Length..].Trim()
//                : null;
//        }

//        protected string ObterEmailDoToken(string token)
//        {
//            string payload = token.Split('.')[1];
//            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
//            byte[] payloadBytes = Convert.FromBase64String(payload);
//            string jsonPayload = Encoding.UTF8.GetString(payloadBytes);

//            try
//            {
//                ViewJwtDto viewJwtDto = JsonConvert.DeserializeObject<ViewJwtDto>(jsonPayload);
//                return viewJwtDto.Email;
//            }
//            catch (Exception)
//            {
//                NotifyError("Erro ao obter o e-mail do token.");
//                return "";
//            }
//        }
//    }
//}