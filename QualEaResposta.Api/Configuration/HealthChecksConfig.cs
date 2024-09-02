using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Configurações de Health Checks para a aplicação.
    /// </summary>
    public static class HealthChecksConfig
    {
        /// <summary>
        /// Configura os Health Checks para a aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddSqlServerHealthCheck(configuration) // Adiciona verificação de saúde para SQL Server
                    .AddMemoryHealthCheck() // Adiciona verificação de saúde para uso de memória
                    .AddDiskSpaceHealthCheck() // Adiciona verificação de saúde para espaço em disco
                    .AddCpuUsageHealthCheck() // Adiciona verificação de saúde para uso de CPU
                    .AddNetworkConnectivityHealthCheck() // Verificação de Conectividade de Rede
                    .AddServerHealthCheck("192.168.0.75", [80, 443, 1433]); // Verificação se o servidor está online e portas estão acessíveis
        }

        /// <summary>
        /// Configura o middleware de Health Checks na aplicação.
        /// </summary>
        /// <param name="app">O pipeline de configuração da aplicação.</param>
        public static void UseHealthChecksConfiguration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/status", new HealthCheckOptions
                {
                    ResponseWriter = WriteHealthCheckResponse
                });
            });
        }

        /// <summary>
        /// Escreve a resposta personalizada do Health Check na resposta HTTP.
        /// </summary>
        /// <param name="context">O contexto HTTP.</param>
        /// <param name="report">O relatório de Health Checks.</param>
        /// <returns>Uma tarefa representando a operação assíncrona de escrita.</returns>
        private static Task WriteHealthCheckResponse(HttpContext context, HealthReport report)
        {
            string result = JsonConvert.SerializeObject(new
            {
                horaAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                statusAplicacao = report.Status.ToString(),
                healthChecks = report.Entries.Select(e => new
                {
                    check = e.Key,
                    errorMessage = e.Value.Exception?.Message ?? e.Value.Description,
                    status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                    duration = e.Value.Duration.ToString()
                })
            });

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(result);
        }
    }

    /// <summary>
    /// Extensões para adicionar verificações de saúde ao HealthChecksBuilder.
    /// </summary>
    public static class HealthChecksBuilderExtensions
    {
        /// <summary>
        /// Adiciona uma verificação de saúde para o SQL Server.
        /// </summary>
        /// <param name="builder">O construtor de Health Checks.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        /// <returns>O construtor de Health Checks com a verificação adicionada.</returns>
        public static IHealthChecksBuilder AddSqlServerHealthCheck(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Connection");

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                builder.AddCheck("Verificação da Conexão com o SQL Server", () =>
                {
                    try
                    {
                        // Testa a conexão com o SQL Server
                        using SqlConnection? connection = new(connectionString);
                        connection.Open();

                        return HealthCheckResult.Healthy("Conexão com o SQL Server está ativa e funcionando corretamente.");
                    }
                    catch (Exception ex)
                    {
                        return HealthCheckResult.Unhealthy("Falha na conexão com o SQL Server.", ex);
                    }
                }, tags: ["db", "data"]);
            }
            else
            {
                builder.AddCheck("sqlserver_nao_configurado", () =>
                    HealthCheckResult.Unhealthy("A string de conexão para o banco de dados SQL Server não está configurada."),
                    tags: ["db", "data"]);
            }

            return builder;
        }

        /// <summary>
        /// Adiciona uma verificação de saúde para o uso de memória.
        /// </summary>
        /// <param name="builder">O construtor de Health Checks.</param>
        /// <returns>O construtor de Health Checks.</returns>
        public static IHealthChecksBuilder AddMemoryHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck("Verificação de Memória", () =>
            {
                long memoryUsed = GC.GetTotalMemory(false);
                int memoryLimit = 1024 * 1024 * 50; // Limite de memória em bytes (50MB)

                if (memoryUsed < memoryLimit)
                {
                    return HealthCheckResult.Healthy($"Uso de memória está normal: {memoryUsed / (1024 * 1024)} MB.");
                }
                else
                {
                    return HealthCheckResult.Degraded($"Uso de memória elevado: {memoryUsed / (1024 * 1024)} MB.");
                }
            }, tags: ["memoria", "recursos"]);

            return builder;
        }

        /// <summary>
        /// Adiciona uma verificação de saúde para o espaço em disco.
        /// </summary>
        /// <param name="builder">O construtor de Health Checks.</param>
        /// <returns>O construtor de Health Checks.</returns>
        public static IHealthChecksBuilder AddDiskSpaceHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck("Verificação de espaço em disco", () =>
            {
                DriveInfo? drive = new("C");
                long availableFreeSpace = drive.AvailableFreeSpace;
                long totalSize = drive.TotalSize;
                double freeSpacePercentage = (double)availableFreeSpace / totalSize * 100;

                if (freeSpacePercentage > 10) // Define um limite de 10% de espaço livre
                {
                    return HealthCheckResult.Healthy($"Espaço em disco suficiente. Espaço livre: {freeSpacePercentage:F2}%.");
                }
                else
                {
                    return HealthCheckResult.Degraded($"Espaço em disco baixo. Espaço livre: {freeSpacePercentage:F2}%."); // Mensagem personalizada
                }
            }, tags: ["disco", "recursos"]);

            return builder;
        }

        /// <summary>
        /// Adiciona uma verificação de saúde para o uso de CPU.
        /// </summary>
        /// <param name="builder">O construtor de Health Checks.</param>
        /// <returns>O construtor de Health Checks.</returns>
        public static IHealthChecksBuilder AddCpuUsageHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck("Verificação do uso da CPU", () =>
            {
                try
                {
                    Process? process = Process.GetCurrentProcess();
                    TimeSpan cpuTime = process.TotalProcessorTime;
                    TimeSpan elapsedTime = DateTime.Now - process.StartTime;
                    double cpuUsagePercentage = (cpuTime.TotalMilliseconds / elapsedTime.TotalMilliseconds) * 100 / Environment.ProcessorCount;

                    float cpuLimit = 80; // Limite de uso da CPU em porcentagem

                    if (cpuUsagePercentage < cpuLimit)
                    {
                        return HealthCheckResult.Healthy($"Uso de CPU está normal: {cpuUsagePercentage:F2}%.");
                    }
                    else
                    {
                        return HealthCheckResult.Degraded($"Uso de CPU elevado: {cpuUsagePercentage:F2}%.");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy("Erro ao verificar o uso de CPU.", ex);
                }
            }, tags: ["cpu", "performance"]);

            return builder;
        }

        /// <summary>
        /// Adiciona uma verificação de saúde para a conectividade de rede.
        /// </summary>
        /// <param name="builder">O construtor de Health Checks.</param>
        /// <returns>O construtor de Health Checks.</returns>
        public static IHealthChecksBuilder AddNetworkConnectivityHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddAsyncCheck("Verificação da Conexão de Rede", async () =>
            {
                try
                {
                    using HttpClient? client = new();
                    HttpResponseMessage? response = await client.GetAsync("https://www.google.com.br");

                    if (response.IsSuccessStatusCode)
                    {
                        return HealthCheckResult.Healthy("Conectividade de rede está OK.");
                    }
                    else
                    {
                        return HealthCheckResult.Degraded("Conectividade de rede está degradada.");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy("Erro ao verificar a conectividade de rede.", ex);
                }
            }, tags: ["rede", "conectividade"]);

            return builder;
        }

        /// <summary>
        /// Adiciona uma verificação de saúde para verificar se um servidor específico na rede está online e acessível, especificando quais portas estão abertas.
        /// </summary>
        /// <param name="builder">O construtor de Health Checks.</param>
        /// <param name="serverIp">O endereço IP do servidor na rede para verificar.</param>
        /// <param name="ports">Uma lista de portas que devem estar abertas para o servidor.</param>
        /// <returns>O construtor de Health Checks com a verificação adicionada.</returns>
        public static IHealthChecksBuilder AddServerHealthCheck(this IHealthChecksBuilder builder, string serverIp, List<int> ports)
        {
            builder.AddCheck($"Verificação do Servidor - {serverIp}", () =>
            {
                try
                {
                    // Verificação ICMP (Ping)
                    using Ping ping = new();
                    PingReply reply = ping.Send(serverIp, 1000); // Timeout de 1000 ms

                    if (reply.Status != IPStatus.Success)
                    {
                        return HealthCheckResult.Unhealthy($"O servidor com IP {serverIp} não está acessível via ping. Status: {reply.Status}");
                    }

                    // Verificação das portas especificadas
                    List<int> portasAbertas = [];
                    List<int> portasFechadas = [];

                    foreach (int port in ports)
                    {
                        using TcpClient tcpClient = new();
                        try
                        {
                            tcpClient.Connect(serverIp, port);
                            portasAbertas.Add(port);
                        }
                        catch (SocketException)
                        {
                            portasFechadas.Add(port);
                        }
                    }

                    if (portasFechadas.Count > 0)
                    {
                        return HealthCheckResult.Degraded(
                            $"O servidor com IP {serverIp} está online, mas algumas portas estão inacessíveis. " +
                            $"Portas abertas: {string.Join(", ", portasAbertas)}. Portas fechadas: {string.Join(", ", portasFechadas)}.");
                    }

                    return HealthCheckResult.Healthy($"O servidor com IP {serverIp} está online e todas as portas especificadas ({string.Join(", ", portasAbertas)}) estão acessíveis.");
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Erro ao verificar o servidor com IP {serverIp}.", ex);
                }
            }, tags: ["servidor", "rede"]);

            return builder;
        }
    }
}