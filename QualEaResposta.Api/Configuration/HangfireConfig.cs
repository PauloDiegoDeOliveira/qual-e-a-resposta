namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para o Hangfire.
    /// </summary>
    public static class HangfireConfig
    {
        /// <summary>
        /// Adiciona a configuração do Hangfire ao contêiner de serviços.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="_">A configuração da aplicação para obter as strings de conexão (não usado atualmente).</param>
        public static void AddHangfireConfiguration(this IServiceCollection services, IConfiguration _)
        {
            services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMemoryStorage()); // Usar armazenamento em memória ou SQL Server para persistência de dados de Hangfire

            services.AddHangfireServer();
        }
    }
}