using QualEaResposta.Benchmarks.Enums;

namespace QualEaResposta.Benchmarks.Mocks
{
    /// <summary>
    /// Mock de <see cref="IWebHostEnvironment"/> para uso em testes de unidade e benchmarks.
    /// Este mock simula o comportamento do ambiente de hospedagem da aplicação.
    /// </summary>
    public class WebHostEnvironmentMock : IWebHostEnvironment
    {
        /// <summary>
        /// Obtém ou define o nome do ambiente de hospedagem da aplicação.
        /// Usa um valor <see cref="EHostingEnvironment"/> convertido para string.
        /// </summary>
        public string EnvironmentName { get; set; } = EHostingEnvironment.Development.ToString();

        /// <summary>
        /// Obtém ou define o nome da aplicação.
        /// </summary>
        public string ApplicationName { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o caminho físico da raiz da web da aplicação.
        /// </summary>
        public string WebRootPath { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o provedor de arquivos para a raiz da web da aplicação.
        /// </summary>
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();

        /// <summary>
        /// Obtém ou define o caminho físico da raiz do conteúdo da aplicação.
        /// </summary>
        public string ContentRootPath { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o provedor de arquivos para a raiz do conteúdo da aplicação.
        /// </summary>
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}