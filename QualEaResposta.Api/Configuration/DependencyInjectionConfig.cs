﻿using Microsoft.AspNetCore.Identity;
using QualEaResposta.Application.DTOs;
using QualEaResposta.Domain.Model;

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
        /// <param name="configuration">O objeto de configuração da aplicação.</param>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
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

            // Registra o MessageHub como um serviço Singleton
            services.AddSingleton<MessageHub>();

            // Acesso ao contexto HTTP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion Serviços Singleton

            #region Serviços Transient

            // Configuração do Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            #endregion Serviços Transient

            #region Configuração de HttpClient

            // Registrar o HttpClient necessário para o ChatGPTService
            services.AddHttpClient<IChatGPTService, ChatGPTService>();

            #endregion Configuração de HttpClient

            #region Configurações de OpenAI a partir do arquivo de configuração (appsettings.json)

            services.Configure<OpenAIConfig>(configuration.GetSection("OpenAIConfig"));

            #endregion Configurações de OpenAI a partir do arquivo de configuração (appsettings.json)

            #region Configuração de Autenticação e Autorização

            services.AddAuthentication()
                    .AddBearerToken(IdentityConstants.BearerScheme); // Configura a autenticação usando tokens Bearer

            services.AddAuthorizationBuilder(); // Adiciona serviços de autorização padrão

            services.AddIdentityCore<Usuario>() // Configura a identidade para a classe Usuario
                    .AddEntityFrameworkStores<ApplicationDbContext>() // Configura a integração com o Entity Framework
                    .AddApiEndpoints(); // Adiciona endpoints da API específicos para a identidade

            #endregion Configuração de Autenticação e Autorização
        }
    }
}