namespace QualEaResposta.Api.V1.Controllers
{
    /// <summary>
    /// Controlador para gerenciar perguntas na versão 1.0 da API.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PerguntasController(IPerguntaService perguntaService,
                                     IChatGPTService chatGPTService,
                                     INotificationService notificationService,
                                     IUser appUser,
                                     ILogger logger) : MainController(notificationService, appUser)
    {
        private readonly IPerguntaService _perguntaService = perguntaService;
        private readonly IChatGPTService _chatGPTService = chatGPTService;
        private readonly ILogger _logger = logger;

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
            _logger.Information("Iniciando a busca pela pergunta com ID {PerguntaId}.", id);

            ViewPerguntaDto? perguntaDTO = await _perguntaService.GetPerguntaByIdAsync(id);

            if (perguntaDTO == null)
            {
                _logger.Warning("Pergunta com ID {PerguntaId} não encontrada.", id);
                NotificarErro("Pergunta não encontrada.");
                return ResponderPadronizado();
            }

            _logger.Information("Pergunta com ID {PerguntaId} encontrada: {@PerguntaDTO}", id, perguntaDTO);
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
            _logger.Information("Recebida solicitação para criar uma nova pergunta.");

            if (!ModelState.IsValid)
            {
                _logger.Warning("ModelState inválido para a pergunta recebida {@PerguntaDTO}.", perguntaDTO);
                return ResponderPadronizado(ModelState);
            }

            if (perguntaDTO == null || string.IsNullOrWhiteSpace(perguntaDTO.TextoPergunta))
            {
                _logger.Warning("A pergunta é obrigatória e não foi fornecida corretamente.");
                NotificarErro("A pergunta é obrigatória.");
                return ResponderPadronizado();
            }

            List<string?>? alternativasTexto = perguntaDTO.Alternativas
                                               .Select(a => a.TextoAlternativa)
                                               .Cast<string?>()
                                               .ToList();

            _logger.Information("Iniciando lógica para criar pergunta e persistir no banco de dados.");

            #region Lógica para criar pergunta e persistir no banco de dados

            // ViewPerguntaDto? createdPerguntaDTO = await _perguntaService.CreatePerguntaAsync(perguntaDTO.TextoPergunta, alternativasTexto);

            // if (createdPerguntaDTO == null)
            // {
            //     _logger.Error("Erro ao criar a pergunta no banco de dados.");
            //     NotificarErro("Erro ao criar a pergunta.");
            //     return ResponderPadronizado();
            // }

            // _logger.Information("Pergunta criada com sucesso no banco de dados: {@CreatedPerguntaDTO}", createdPerguntaDTO);
            // NotificarMensagem("Pergunta criada com sucesso.");
            // return ResponderPadronizado(createdPerguntaDTO);

            #endregion Lógica para criar pergunta e persistir no banco de dados

            _logger.Information("Iniciando lógica para obter resposta do ChatGPT.");

            #region Lógica para obter resposta do ChatGPT e retornar ao cliente

            // Chama o serviço do ChatGPT para obter a resposta correta com base na pergunta e alternativas
            string resposta = await _chatGPTService.GetCorrectAnswerAsync(perguntaDTO.TextoPergunta, alternativasTexto);

            // Verifica se a resposta obtida está vazia ou nula, indicando uma falha ao obter a resposta do ChatGPT
            if (string.IsNullOrWhiteSpace(resposta))
            {
                _logger.Error("Não foi possível obter uma resposta para a pergunta do ChatGPT.");
                NotificarErro("Não foi possível obter uma resposta para a pergunta.");
                return ResponderPadronizado();
            }

            _logger.Information("Resposta obtida com sucesso do ChatGPT. Resposta: {Resposta}", resposta);
            NotificarMensagem("Resposta obtida com sucesso.");
            return ResponderPadronizado(resposta);

            #endregion Lógica para obter resposta do ChatGPT e retornar ao cliente
        }
    }
}