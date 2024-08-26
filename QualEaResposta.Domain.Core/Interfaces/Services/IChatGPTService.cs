namespace QualEaResposta.Domain.Core.Interfaces.Services
{
    public interface IChatGPTService
    {
        Task<string> GetCorrectAnswerAsync(string question, List<string?> alternatives);
    }
}