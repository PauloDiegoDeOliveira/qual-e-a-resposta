namespace QualEaResposta.Api.V1.Controllers
{
    /// <summary>
    /// Controlador para gerenciar perguntas na versão 1.0 da API.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="PerguntasController"/>.
    /// </remarks>
    /// <param name="perguntaService">Serviço de perguntas.</param>
    /// <param name="chatGPTService">Serviço para interagir com o ChatGPT.</param>
    /// <param name="notificationService">Serviço de notificação.</param>
    /// <param name="appUser">Informações sobre o usuário autenticado.</param>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PerguntasController(IPerguntaService perguntaService,
                                     IChatGPTService chatGPTService,
                                     INotificationService notificationService,
                                     IUser appUser) : MainController(notificationService, appUser)
    {
        private readonly IPerguntaService _perguntaService = perguntaService;
        private readonly IChatGPTService _chatGPTService = chatGPTService;

        /// <summary>
        /// Obtém uma pergunta pelo ID.
        /// </summary>
        /// <param name="id">ID da pergunta.</param>
        /// <returns>Pergunta correspondente ao ID fornecido.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ViewPerguntaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPerguntaById(Guid id)
        {
            ViewPerguntaDto? perguntaDTO = await _perguntaService.GetPerguntaByIdAsync(id);
            if (perguntaDTO == null)
            {
                NotificarErro("Pergunta não encontrada.");
                return ResponderPadronizado();
            }

            return ResponderPadronizado(perguntaDTO);
        }

        /// <summary>
        /// Cria uma nova pergunta.
        /// </summary>
        /// <param name="perguntaDTO">DTO da pergunta.</param>
        /// <returns>Resultado da criação da pergunta.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ViewPerguntaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePergunta([FromBody] PostPerguntaDto perguntaDTO)
        {
            if (!ModelState.IsValid)
            {
                return ResponderPadronizado(ModelState);
            }

            if (perguntaDTO == null || string.IsNullOrWhiteSpace(perguntaDTO.TextoPergunta))
            {
                NotificarErro("A pergunta é obrigatória.");
                return ResponderPadronizado();
            }

            List<string?>? alternativasTexto = perguntaDTO.Alternativas
                                               .Select(a => a.TextoAlternativa)
                                               .Cast<string?>()
                                               .ToList();

            #region Lógica para criar pergunta e persistir no banco de dados

            //ViewPerguntaDto? createdPerguntaDTO = await _perguntaService.CreatePerguntaAsync(perguntaDTO.TextoPergunta, alternativasTexto);

            //if (createdPerguntaDTO == null)
            //{
            //    NotificarErro("Erro ao criar a pergunta.");
            //    return ResponderPadronizado();
            //}

            //NotificarMensagem("Pergunta criada com sucesso.");
            //return ResponderPadronizado(createdPerguntaDTO);

            #endregion Lógica para criar pergunta e persistir no banco de dados

            #region Lógica para obter resposta do ChatGPT e retornar ao cliente

            // Chama o serviço do ChatGPT para obter a resposta correta com base na pergunta e alternativas
            string resposta = await _chatGPTService.GetCorrectAnswerAsync(perguntaDTO.TextoPergunta, alternativasTexto);

            // Verifica se a resposta obtida está vazia ou nula, indicando uma falha ao obter a resposta do ChatGPT
            if (string.IsNullOrWhiteSpace(resposta))
            {
                NotificarErro("Não foi possível obter uma resposta para a pergunta.");
                return ResponderPadronizado();
            }

            // Notifica sucesso e retorna a resposta do ChatGPT
            NotificarMensagem("Resposta obtida com sucesso.");
            return ResponderPadronizado(resposta);

            #endregion Lógica para obter resposta do ChatGPT e retornar ao cliente
        }
    }
}