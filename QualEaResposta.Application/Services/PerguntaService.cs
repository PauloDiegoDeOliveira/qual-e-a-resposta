namespace QualEaResposta.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar operações relacionadas a perguntas.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="PerguntaService"/>.
    /// </remarks>
    /// <param name="perguntaRepository">O repositório para gerenciamento de perguntas.</param>
    /// <param name="chatGPTService">O serviço para interagir com o ChatGPT para obter respostas.</param>
    /// <param name="mapper">O mapeador para converter entidades para DTOs.</param>
    /// <param name="logger">Logger para registrar eventos e erros.</param>
    public class PerguntaService(IPerguntaRepository perguntaRepository, IChatGPTService chatGPTService, IMapper mapper, ILogger<PerguntaService> logger) : IPerguntaService
    {
        private readonly IPerguntaRepository _perguntaRepository = perguntaRepository;
        private readonly IChatGPTService _chatGPTService = chatGPTService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<PerguntaService> _logger = logger;

        /// <summary>
        /// Cria uma nova pergunta e suas alternativas associadas.
        /// </summary>
        /// <param name="textoPergunta">O texto da pergunta.</param>
        /// <param name="alternativasTexto">A lista de textos de alternativas associadas à pergunta.</param>
        /// <returns>Um <see cref="Task"/> que representa a operação assíncrona, contendo o DTO da pergunta criada.</returns>
        public async Task<ViewPerguntaDto> CreatePerguntaAsync(string textoPergunta, List<string?> alternativasTexto)
        {
            _logger.LogInformation("Iniciando o processo de criação de uma nova pergunta.");

            // Lógica para obter a resposta correta usando ChatGPT
            _logger.LogInformation("Chamando o serviço ChatGPT para obter a resposta correta.");
            string respostaCorretaTexto = await _chatGPTService.GetCorrectAnswerAsync(textoPergunta, alternativasTexto);

            _logger.LogInformation("Resposta correta obtida: {RespostaCorreta}", respostaCorretaTexto);

            // Criação de alternativas com base na resposta correta
            List<Alternativa> alternativas = alternativasTexto
                .Where(texto => !string.IsNullOrWhiteSpace(texto))
                .Select(texto => new Alternativa
                {
                    TextoAlternativa = texto!,
                    Correta = texto == respostaCorretaTexto
                }).ToList();

            _logger.LogInformation("Alternativas criadas com base na resposta correta.");

            // Criação da entidade Pergunta
            Pergunta pergunta = new()
            {
                TextoPergunta = textoPergunta,
                Alternativas = alternativas
            };

            _logger.LogInformation("Entidade Pergunta criada.");

            // Salvamento da pergunta no repositório
            await _perguntaRepository.AddAsync(pergunta);
            await _perguntaRepository.SaveChangesAsync();

            _logger.LogInformation("Pergunta salva no repositório com sucesso.");

            // Conversão da entidade Pergunta para DTO de visualização
            ViewPerguntaDto viewPerguntaDto = _mapper.Map<ViewPerguntaDto>(pergunta);
            _logger.LogInformation("Pergunta convertida para DTO de visualização.");

            return viewPerguntaDto;
        }

        /// <summary>
        /// Obtém uma pergunta pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único da pergunta.</param>
        /// <returns>Um <see cref="Task"/> que representa a operação assíncrona, contendo o DTO da pergunta ou null se não for encontrada.</returns>
        public async Task<ViewPerguntaDto?> GetPerguntaByIdAsync(Guid id)
        {
            _logger.LogInformation("Iniciando busca pela pergunta com ID {PerguntaId}.", id);

            Pergunta? pergunta = await _perguntaRepository.GetByIdAsync(id);

            if (pergunta == null)
            {
                _logger.LogWarning("Pergunta com ID {PerguntaId} não encontrada.", id);
                return null;
            }

            _logger.LogInformation("Pergunta com ID {PerguntaId} encontrada. Convertendo para DTO.", id);

            return _mapper.Map<ViewPerguntaDto>(pergunta);
        }
    }
}