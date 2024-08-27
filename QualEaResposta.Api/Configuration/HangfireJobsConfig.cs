using QualEaResposta.Application.Jobs;

namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para registro de jobs do Hangfire.
    /// </summary>
    public static class HangfireJobsConfig
    {
        /// <summary>
        /// Registra os jobs recorrentes no Hangfire.
        /// </summary>
        /// <param name="recurringJobManager">O gerenciador de jobs recorrentes.</param>
        public static void RegisterJobs(IRecurringJobManager recurringJobManager)
        {
            // Registra um job recorrente para verificar a execução
            recurringJobManager.AddOrUpdate(
                "RegistrarDataHoraJob",
                () => PerguntaJobs.RegistrarDataHoraAsync(),
                Cron.Minutely); // Executa a cada minuto
        }
    }
}