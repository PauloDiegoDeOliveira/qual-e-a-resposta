using QualEaResposta.Domain.Core.Interfaces.Repositories;

namespace QualEaResposta.Infrastructure.Data.Repositories
{
    public class PerguntaRepository(ApplicationDbContext context) : IPerguntaRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task<Pergunta> GetByIdAsync(Guid id)
        {
            return await context.Perguntas.Include(p => p.Alternativas).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pergunta>> GetAllAsync()
        {
            return await context.Perguntas.Include(p => p.Alternativas).ToListAsync();
        }

        public async Task AddAsync(Pergunta pergunta)
        {
            await context.Perguntas.AddAsync(pergunta);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}