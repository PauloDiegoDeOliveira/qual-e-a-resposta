namespace QualEaResposta.Application.Services
{
    public class PerguntaService(
        IPerguntaRepository perguntaRepository,
        IChatGPTService chatGPTService,
        IMapper mapper) : IPerguntaService
    {
        private readonly IPerguntaRepository _perguntaRepository = perguntaRepository;
        private readonly IChatGPTService _chatGPTService = chatGPTService;
        private readonly IMapper _mapper = mapper;

        public async Task<PerguntaDTO> CreatePerguntaAsync(string textoPergunta, List<string?> alternativasTexto)
        {
            string respostaCorretaTexto = await _chatGPTService.GetCorrectAnswerAsync(textoPergunta, alternativasTexto);

            List<Alternativa> alternativas = alternativasTexto.Select(texto => new Alternativa
            {
                TextoAlternativa = texto,
                ECorreta = texto == respostaCorretaTexto
            }).ToList();

            Pergunta pergunta = new()
            {
                TextoPergunta = textoPergunta,
                Alternativas = alternativas
            };

            await _perguntaRepository.AddAsync(pergunta);
            await _perguntaRepository.SaveChangesAsync();

            // Usa o AutoMapper para converter Pergunta em PerguntaDTO
            return _mapper.Map<PerguntaDTO>(pergunta);
        }

        public async Task<PerguntaDTO> GetPerguntaByIdAsync(Guid id)
        {
            var pergunta = await _perguntaRepository.GetByIdAsync(id);
            // Usa o AutoMapper para converter Pergunta em PerguntaDTO
            return _mapper.Map<PerguntaDTO>(pergunta);
        }
    }
}