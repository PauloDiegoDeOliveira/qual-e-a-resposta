namespace QualEaResposta.Benchmarks.Mocks
{
    /// <summary>
    /// Mock de <see cref="IHubContext{T}"/> para uso em testes de unidade e benchmarks.
    /// Este mock simula o comportamento do contexto de um Hub SignalR para fins de teste.
    /// </summary>
    public class HubContextMock : IHubContext<MessageHub>
    {
        /// <summary>
        /// Obtém um conjunto de proxies para clientes conectados ao hub.
        /// </summary>
        public IHubClients Clients => new HubClientsMock();

        /// <summary>
        /// Obtém o gerenciador de grupos do hub.
        /// Este membro não é implementado no mock e lança uma exceção se for chamado.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// Lançado sempre que este membro é acessado, pois não é implementado no mock.
        /// </exception>
        public IGroupManager Groups => throw new NotImplementedException();
    }
}