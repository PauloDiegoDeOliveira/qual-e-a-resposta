namespace QualEaResposta.Domain.Core.Interfaces.Repositories
{
    public interface IPerguntaRepository
    {
        Task<Pergunta> GetByIdAsync(Guid id); // Não permite retorno nulo

        Task<IEnumerable<Pergunta>> GetAllAsync();

        Task AddAsync(Pergunta pergunta);

        Task SaveChangesAsync();
    }
}