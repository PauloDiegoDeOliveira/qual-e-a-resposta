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
    public class PerguntaService(IPerguntaRepository perguntaRepository, IChatGPTService chatGPTService, IMapper mapper) : IPerguntaService
    {
        private readonly IPerguntaRepository _perguntaRepository = perguntaRepository;
        private readonly IChatGPTService _chatGPTService = chatGPTService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Cria uma nova pergunta e suas alternativas associadas.
        /// </summary>
        /// <param name="textoPergunta">O texto da pergunta.</param>
        /// <param name="alternativasTexto">A lista de textos de alternativas associadas à pergunta.</param>
        /// <returns>Um <see cref="Task"/> que representa a operação assíncrona, contendo o DTO da pergunta criada.</returns>
        public async Task<ViewPerguntaDto> CreatePerguntaAsync(string textoPergunta, List<string?> alternativasTexto)
        {
            // Lógica para obter a resposta correta usando ChatGPT
            string respostaCorretaTexto = await _chatGPTService.GetCorrectAnswerAsync(textoPergunta, alternativasTexto);

            // Criação de alternativas com base na resposta correta
            List<Alternativa> alternativas = alternativasTexto
                .Where(texto => !string.IsNullOrWhiteSpace(texto))
                .Select(texto => new Alternativa
                {
                    TextoAlternativa = texto!,
                    ECorreta = texto == respostaCorretaTexto
                }).ToList();

            // Criação da entidade Pergunta
            Pergunta pergunta = new()
            {
                TextoPergunta = textoPergunta,
                Alternativas = alternativas
            };

            // Salvamento da pergunta no repositório
            await _perguntaRepository.AddAsync(pergunta);
            await _perguntaRepository.SaveChangesAsync();

            // Conversão da entidade Pergunta para DTO de visualização
            return _mapper.Map<ViewPerguntaDto>(pergunta);
        }

        /// <summary>
        /// Obtém uma pergunta pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único da pergunta.</param>
        /// <returns>Um <see cref="Task"/> que representa a operação assíncrona, contendo o DTO da pergunta ou null se não for encontrada.</returns>
        public async Task<ViewPerguntaDto?> GetPerguntaByIdAsync(Guid id)
        {
            var pergunta = await _perguntaRepository.GetByIdAsync(id);

            if (pergunta == null)
                return null;

            return _mapper.Map<ViewPerguntaDto>(pergunta);
        }
    }
}