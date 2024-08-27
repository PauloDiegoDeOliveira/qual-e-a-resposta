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

            services.AddScoped<IPerguntaService, PerguntaService>();
            services.AddScoped<PerguntaDomainService>();
            services.AddScoped<IPerguntaRepository, PerguntaRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificador, NotificationService>();
            services.AddScoped<IUser, AspNetUserService>();

            #endregion Serviços Scoped

            #region Serviços Singleton

            // Acesso ao contexto HTTP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion Serviços Singleton

            #region Serviços Transient

            // Configuração do Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            #endregion Serviços Transient

            #region Outros Serviços

            // Registrar o HttpClient necessário para o ChatGPTService
            services.AddHttpClient<IChatGPTService, ChatGPTService>();

            #endregion Outros Serviços
        }
    }
}