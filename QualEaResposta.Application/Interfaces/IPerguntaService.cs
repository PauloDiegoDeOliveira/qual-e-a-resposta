namespace QualEaResposta.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de perguntas.
    /// </summary>
    public interface IPerguntaService
    {
        /// <summary>
        /// Cria uma nova pergunta com base no texto da pergunta e nas alternativas fornecidas.
        /// </summary>
        /// <param name="textoPergunta">O texto da pergunta a ser criada.</param>
        /// <param name="alternativasTexto">Lista de textos de alternativas associadas à pergunta.</param>
        /// <returns>Um <see cref="Task"/> que representa o resultado da operação, contendo o DTO da pergunta criada.</returns>
        Task<ViewPerguntaDto> CreatePerguntaAsync(string textoPergunta, List<string?> alternativasTexto);

        /// <summary>
        /// Obtém uma pergunta pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único da pergunta.</param>
        /// <returns>Um <see cref="Task"/> que representa o resultado da operação, contendo o DTO da pergunta.</returns>
        Task<ViewPerguntaDto?> GetPerguntaByIdAsync(Guid id);
    }
}