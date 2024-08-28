namespace QualEaResposta.Benchmarks.Mocks
{
    /// <summary>
    /// Mock de <see cref="IHubClients"/> para uso em testes de unidade e benchmarks.
    /// Este mock simula o comportamento de um conjunto de clientes SignalR para fins de teste.
    /// </summary>
    public class HubClientsMock : IHubClients
    {
        /// <summary>
        /// Obtém um proxy para todos os clientes conectados.
        /// </summary>
        public IClientProxy All => new ClientProxyMock();

        /// <summary>
        /// Obtém um proxy para todos os clientes conectados, exceto aqueles especificados.
        /// </summary>
        /// <param name="excludedConnectionIds">Lista de IDs de conexão a serem excluídos.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa os clientes conectados, excluindo os especificados.</returns>
        public IClientProxy AllExcept(IReadOnlyList<string> excludedConnectionIds)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para um cliente específico identificado pelo ID de conexão.
        /// </summary>
        /// <param name="connectionId">O ID de conexão do cliente.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa o cliente específico.</returns>
        public IClientProxy Client(string connectionId)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para um conjunto de clientes específicos identificados pelos IDs de conexão.
        /// </summary>
        /// <param name="connectionIds">Lista de IDs de conexão dos clientes.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa o conjunto de clientes específicos.</returns>
        public IClientProxy Clients(IReadOnlyList<string> connectionIds)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para todos os clientes de um grupo específico.
        /// </summary>
        /// <param name="groupName">O nome do grupo.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa os clientes do grupo específico.</returns>
        public IClientProxy Group(string groupName)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para todos os clientes de um conjunto de grupos específicos.
        /// </summary>
        /// <param name="groupNames">Lista de nomes dos grupos.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa os clientes dos grupos específicos.</returns>
        public IClientProxy Groups(IReadOnlyList<string> groupNames)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para todos os clientes de um grupo específico, excluindo os especificados.
        /// </summary>
        /// <param name="groupName">O nome do grupo.</param>
        /// <param name="excludedConnectionIds">Lista de IDs de conexão a serem excluídos.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa os clientes do grupo, excluindo os especificados.</returns>
        public IClientProxy GroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para um usuário específico identificado pelo ID de usuário.
        /// </summary>
        /// <param name="userId">O ID do usuário.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa o usuário específico.</returns>
        public IClientProxy User(string userId)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }

        /// <summary>
        /// Obtém um proxy para um conjunto de usuários específicos identificados pelos IDs de usuário.
        /// </summary>
        /// <param name="userIds">Lista de IDs de usuários.</param>
        /// <returns>Um <see cref="IClientProxy"/> que representa o conjunto de usuários específicos.</returns>
        public IClientProxy Users(IReadOnlyList<string> userIds)
        {
            return new ClientProxyMock(); // Simulação de retorno
        }
    }
}