namespace QualEaResposta.Benchmarks.Mocks
{
    /// <summary>
    /// Mock de <see cref="IClientProxy"/> para uso em testes de unidade e benchmarks.
    /// Este mock simula o comportamento de um cliente SignalR para fins de teste.
    /// </summary>
    public class ClientProxyMock : IClientProxy
    {
        /// <summary>
        /// Simula o envio de uma mensagem para o cliente.
        /// </summary>
        /// <param name="method">O nome do método a ser chamado no cliente.</param>
        /// <param name="args">Os argumentos para o método, podendo ser nulos.</param>
        /// <param name="cancellationToken">Um token de cancelamento para cancelar a operação, se necessário.</param>
        /// <returns>
        /// Um <see cref="Task"/> representando a operação assíncrona de envio de mensagem,
        /// que neste mock é concluída instantaneamente.
        /// </returns>
        public Task SendCoreAsync(string method, object?[] args, CancellationToken cancellationToken = default)
        {
            // Simulação de envio de mensagem
            return Task.CompletedTask;
        }
    }
}