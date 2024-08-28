namespace QualEaResposta.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="PerguntaRepository"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    /// <param name="logger">Logger para registrar eventos e erros.</param>
    public class PerguntaRepository(ApplicationDbContext context, ILogger<PerguntaRepository> logger) : IPerguntaRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<PerguntaRepository> _logger = logger;

        public async Task<Pergunta> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Buscando a pergunta com ID {PerguntaId}.", id);

            var pergunta = await _context.Perguntas
                .Include(p => p.Alternativas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pergunta == null)
            {
                _logger.LogWarning("Pergunta com ID {PerguntaId} não encontrada.", id);
                return new Pergunta(); // Retorna um novo objeto Pergunta se não encontrar
            }

            _logger.LogInformation("Pergunta com ID {PerguntaId} encontrada.", id);
            return pergunta;
        }

        public async Task<IEnumerable<Pergunta>> GetAllAsync()
        {
            _logger.LogInformation("Buscando todas as perguntas.");

            var perguntas = await _context.Perguntas
                .Include(p => p.Alternativas)
                .ToListAsync();

            _logger.LogInformation("Total de {TotalPerguntas} perguntas encontradas.", perguntas.Count);
            return perguntas;
        }

        public async Task AddAsync(Pergunta pergunta)
        {
            if (pergunta == null)
            {
                _logger.LogWarning("Tentativa de adicionar uma pergunta nula. Operação ignorada.");
                return;
            }

            _logger.LogInformation("Adicionando uma nova pergunta ao contexto.");
            await _context.Perguntas.AddAsync(pergunta);
        }

        public async Task SaveChangesAsync()
        {
            _logger.LogInformation("Salvando mudanças no banco de dados.");
            await _context.SaveChangesAsync();
            _logger.LogInformation("Mudanças salvas com sucesso.");
        }
    }
}