namespace QualEaResposta.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Serviços Scoped

            services.AddScoped<IPerguntaService, PerguntaService>();
            services.AddScoped<PerguntaDomainService>();
            services.AddScoped<IPerguntaRepository, PerguntaRepository>();
            services.AddScoped<IChatGPTService, ChatGPTService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUser, AspNetUserService>();
            services.AddScoped<INotificador, NotificationService>();

            #endregion Serviços Scoped

            #region Serviços Singleton

            // Acesso ao contexto HTTP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion Serviços Singleton

            #region Serviços Transient

            // Configuração do Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            #endregion Serviços Transient
        }
    }
}