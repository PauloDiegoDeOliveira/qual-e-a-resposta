namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para a base de dados.
    /// </summary>
    public static class DataBaseConfig
    {
        /// <summary>
        /// Adiciona a configuração da base de dados ao serviço de injeção de dependências.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="configuration">As configurações da aplicação.</param>
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
        }

        /// <summary>
        /// Configura o contexto do banco de dados.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="configuration">As configurações da aplicação.</param>
        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Configura o DbContext para usar o SQL Server com a string de conexão fornecida.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));
        }

        /// <summary>
        /// Usa a configuração do banco de dados e aplica migrações pendentes.
        /// </summary>
        /// <param name="app">O aplicativo que está sendo configurado.</param>
        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using ApplicationDbContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Aplica migrações pendentes automaticamente ao iniciar o aplicativo.
            context.Database.Migrate();
        }
    }
}