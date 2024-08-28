namespace QualEaResposta.Benchmarks.V1.Controllers
{
    /// <summary>
    /// Classe de benchmark para testar o desempenho do controlador <see cref="VersaoController"/>.
    /// Utiliza o BenchmarkDotNet para medir o tempo de execução dos métodos do controlador.
    /// </summary>
    public class VersaoControllerBenchmark
    {
        /// <summary>
        /// Instância do controlador de versão usada para benchmarks.
        /// </summary>
        private VersaoController _controller = null!; // Inicializa com um valor padrão null-forgiving

        /// <summary>
        /// Configuração inicial para o benchmark, incluindo a criação de mocks para as dependências do controlador.
        /// Este método é executado uma vez antes de qualquer benchmark.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            // Configurar mocks necessários para as dependências do controlador
            var environmentMock = new WebHostEnvironmentMock { EnvironmentName = "Development" };
            var notificationServiceMock = new NotificationServiceMock();
            var userMock = new UserMock();
            var hubContextMock = new HubContextMock();

            _controller = new VersaoController(environmentMock, notificationServiceMock, userMock, hubContextMock);
        }

        /// <summary>
        /// Método de benchmark para testar o desempenho do método <see cref="VersaoController.ValorAsync"/>.
        /// Este método mede o tempo necessário para executar o método de obtenção de versão da API.
        /// </summary>
        /// <returns>Uma tarefa que representa a operação assíncrona de benchmark.</returns>
        [Benchmark]
        public async Task BenchmarkValorAsync()
        {
            var apiVersion = new ApiVersion(1, 0);
            await _controller.ValorAsync(apiVersion);
        }
    }
}