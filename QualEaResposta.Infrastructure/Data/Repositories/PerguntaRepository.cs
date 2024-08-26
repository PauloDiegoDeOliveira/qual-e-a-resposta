namespace QualEaResposta.Infrastructure.Data.Repositories
{
    public class PerguntaRepository(ApplicationDbContext context) : IPerguntaRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Pergunta> GetByIdAsync(Guid id)
        {
            var pergunta = await _context.Perguntas
                .Include(p => p.Alternativas)
                .FirstOrDefaultAsync(p => p.Id == id);

            return pergunta ?? new Pergunta(); // Retorna um novo objeto Pergunta se não encontrar
        }

        public async Task<IEnumerable<Pergunta>> GetAllAsync()
        {
            return await _context.Perguntas
                .Include(p => p.Alternativas)
                .ToListAsync();
        }

        public async Task AddAsync(Pergunta pergunta)
        {
            if (pergunta == null)
            {
                // Se a pergunta for nula, não faz nada
                return;
            }

            await _context.Perguntas.AddAsync(pergunta);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}