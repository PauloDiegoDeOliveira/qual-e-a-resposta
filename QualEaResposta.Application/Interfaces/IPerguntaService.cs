namespace QualEaResposta.Application.Interfaces
{
    public interface IPerguntaService
    {
        Task<PerguntaDTO> CreatePerguntaAsync(string textoPergunta, List<string?> alternativasTexto);

        Task<PerguntaDTO> GetPerguntaByIdAsync(Guid id);
    }
}