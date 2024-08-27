namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Configuração de injeção de dependência para os serviços da aplicação.
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Adiciona a configuração de injeção de dependência para os serviços da aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Serviços Scoped

            // Serviços de Aplicação
            services.AddScoped<IPerguntaService, PerguntaService>();

            // Serviços de Domínio
            services.AddScoped<PerguntaDomainService>();

            // Repositórios e Serviços de Infraestrutura
            services.AddScoped<IPerguntaRepository, PerguntaRepository>();
            services.AddScoped<IChatGPTService, ChatGPTService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificador, NotificationService>(); // Se NotificationService implementa ambas as interfaces
            services.AddScoped<IUser, AspNetUserService>();

            #endregion Serviços Scoped

            #region Serviços Singleton

            // Acesso ao contexto HTTP - Singleton é apropriado pois deve haver apenas um contexto HTTP por requisição
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion Serviços Singleton

            #region Serviços Transient

            // Configuração do Swagger - Transient é apropriado para objetos de curta duração
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            #endregion Serviços Transient
        }
    }
}