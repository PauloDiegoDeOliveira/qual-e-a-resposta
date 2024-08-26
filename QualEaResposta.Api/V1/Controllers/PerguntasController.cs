namespace QualEaResposta.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PerguntasController(IPerguntaService perguntaService,
                                     INotificationService notificationService,
                                     IUser appUser) : MainController(notificationService, appUser)
    {
        private readonly IPerguntaService _perguntaService = perguntaService;

        /// <summary>
        /// Obtem uma pergunta pelo ID.
        /// </summary>
        /// <param name="id">ID da pergunta</param>
        /// <returns>Pergunta correspondente ao ID fornecido</returns>
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
        /// <param name="perguntaDTO">DTO da pergunta</param>
        /// <returns>Resultado da criação da pergunta</returns>
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

            ViewPerguntaDto? createdPerguntaDTO = await _perguntaService.CreatePerguntaAsync(perguntaDTO.TextoPergunta, alternativasTexto);

            if (createdPerguntaDTO == null)
            {
                NotificarErro("Erro ao criar a pergunta.");
                return ResponderPadronizado();
            }

            NotificarMensagem("Pergunta criada com sucesso.");
            return ResponderPadronizado(createdPerguntaDTO);
        }
    }
}