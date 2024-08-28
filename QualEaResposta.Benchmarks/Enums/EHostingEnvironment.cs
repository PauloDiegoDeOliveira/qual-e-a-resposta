namespace QualEaResposta.Benchmarks.Enums
{
    /// <summary>
    /// Enumeração para representar os diferentes ambientes de hospedagem.
    /// </summary>
    public enum EHostingEnvironment
    {
        /// <summary>
        /// Ambiente de desenvolvimento, utilizado para desenvolvimento e testes locais.
        /// </summary>
        Development = 1,

        /// <summary>
        /// Ambiente de staging, utilizado para testes de integração e pré-produção.
        /// </summary>
        Staging,

        /// <summary>
        /// Ambiente de produção, utilizado para acesso dos usuários finais.
        /// </summary>
        Production,

        /// <summary>
        /// Ambiente de teste, utilizado especificamente para testes de unidade e de integração.
        /// </summary>
        Testing
    }
}