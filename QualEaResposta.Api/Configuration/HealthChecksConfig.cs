namespace QualEaResposta.Api.Configuration
{
    public static class HealthChecksConfig
    {
        private static readonly string[] tags = ["db", "data"];

        public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                // Se a string de conexão for válida, adiciona o HealthCheck do SQL Server
                services.AddHealthChecks()
                    .AddSqlServer(
                        connectionString,
                        name: "sqlserver",
                        tags: tags);
            }
            else
            {
                // Se a string de conexão for nula ou vazia, adiciona um HealthCheck alternativo
                services.AddHealthChecks()
                    .AddCheck("sqlserver_not_configured", () =>
                        HealthCheckResult.Unhealthy("A string de conexão para o banco de dados SQL Server não está configurada."),
                        tags);
            }
        }

        public static void UseHealthChecksConfiguration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/status",
                    new HealthCheckOptions()
                    {
                        ResponseWriter = async (context, report) =>
                        {
                            string result = JsonConvert.SerializeObject(
                                new
                                {
                                    horaAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    statusAplicacao = report.Status.ToString(),
                                    healthChecks = report.Entries.Select(e => new
                                    {
                                        check = e.Key,
                                        ErrorMessage = e.Value.Exception?.Message,
                                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                        duration = e.Value.Duration.ToString()
                                    })
                                });
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync(result);
                        }
                    })/*.RequireAuthorization()*/;
            });
        }
    }
}