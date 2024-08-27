using Microsoft.AspNetCore.Cors.Infrastructure;

namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para políticas CORS.
    /// </summary>
    public static class CorsConfig
    {
        /// <summary>
        /// Adiciona configurações de CORS ao serviço de injeção de dependências.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="environment">O ambiente de hospedagem da aplicação.</param>
        public static void AddCorsConfiguration(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddCors(options =>
            {
                // Configuração comum para todas as políticas
                void ConfigurePolicy(CorsPolicyBuilder builder)
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();

                    if (environment.IsProduction())
                    {
                        // Configuração temporária para permitir todas as origens em produção
                        builder.AllowAnyOrigin(); // TODO: Alterar para "WithOrigins("https://trustedorigin.com")" para maior segurança em produção
                    }
                    else
                    {
                        // Configuração para outros ambientes
                        builder.SetIsOriginAllowed(origin => true);
                    }
                }

                // Define as políticas de CORS
                options.AddPolicy("Development", ConfigurePolicy);
                options.AddPolicy("Production", ConfigurePolicy);
                options.AddPolicy("Staging", ConfigurePolicy);
            });
        }
    }
}