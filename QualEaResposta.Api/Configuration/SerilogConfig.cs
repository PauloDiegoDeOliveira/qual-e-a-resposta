namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para o Serilog.
    /// </summary>
    public static class SerilogConfig
    {
        private const string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";
        private const string DefaultEnvironment = "Production";
        private const string AppSettingsFile = "appsettings.json";
        private const string AppSettingsEnvironmentFile = "appsettings.{0}.json";

        /// <summary>
        /// Configura o Serilog como o logger principal da aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        public static void AddSerilogApi(IServiceCollection services)
        {
            var configuration = GetConfiguration();
            LogConfiguration(configuration);

            // Adiciona Serilog ao IServiceCollection para integrá-lo com a infraestrutura do ASP.NET Core
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));
        }

        /// <summary>
        /// Configura o Serilog usando as configurações fornecidas.
        /// </summary>
        /// <param name="configurationBuilder">As configurações da aplicação.</param>
        private static void LogConfiguration(IConfigurationRoot configurationBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationBuilder)
                .CreateLogger();
        }

        /// <summary>
        /// Obtém a configuração da aplicação com base no ambiente atual.
        /// </summary>
        /// <returns>As configurações carregadas.</returns>
        private static IConfigurationRoot GetConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable(EnvironmentVariable) ?? DefaultEnvironment;

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsFile)
                .AddJsonFile(string.Format(AppSettingsEnvironmentFile, environment), optional: true)
                .Build();
        }
    }
}