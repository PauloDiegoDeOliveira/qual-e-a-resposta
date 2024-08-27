namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Configurações de Health Checks para a aplicação.
    /// </summary>
    public static class HealthChecksConfig
    {
        private static readonly string[] tags = ["db", "data"];

        /// <summary>
        /// Configura os Health Checks para a aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                // Adiciona HealthCheck para o SQL Server se a string de conexão for válida
                services.AddHealthChecks()
                    .AddSqlServer(
                        connectionString,
                        name: "sqlserver",
                        tags: tags);
            }
            else
            {
                // Adiciona um HealthCheck alternativo se a string de conexão estiver ausente
                services.AddHealthChecks()
                    .AddCheck("sqlserver_not_configured", () =>
                        HealthCheckResult.Unhealthy("A string de conexão para o banco de dados SQL Server não está configurada."),
                        tags);
            }
        }

        /// <summary>
        /// Configura o middleware de Health Checks na aplicação.
        /// </summary>
        /// <param name="app">O pipeline de configuração da aplicação.</param>
        public static void UseHealthChecksConfiguration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/status",
                    new HealthCheckOptions
                    {
                        ResponseWriter = async (context, report) =>
                        {
                            var result = JsonConvert.SerializeObject(new
                            {
                                horaAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                statusAplicacao = report.Status.ToString(),
                                healthChecks = report.Entries.Select(e => new
                                {
                                    check = e.Key,
                                    errorMessage = e.Value.Exception?.Message,
                                    status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                    duration = e.Value.Duration.ToString()
                                })
                            });

                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync(result);
                        }
                    }); //.RequireAuthorization(); // Descomentado para usar autenticação, se necessário
            });
        }
    }
}