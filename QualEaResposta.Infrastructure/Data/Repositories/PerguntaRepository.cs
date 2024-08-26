using QualEaResposta.Domain.Core.Interfaces.Repositories;

namespace QualEaResposta.Infrastructure.Data.Repositories
{
    public class PerguntaRepository(ApplicationDbContext context) : IPerguntaRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Pergunta?> GetByIdAsync(Guid id)
        {
            // Use "?" para indicar que pode retornar null
            return await _context.Perguntas
                .Include(p => p.Alternativas)
                .FirstOrDefaultAsync(p => p.Id == id);
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
                throw new ArgumentNullException(nameof(pergunta)); // Verifica se o objeto não é nulo
            }

            await _context.Perguntas.AddAsync(pergunta);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}