namespace QualEaResposta.Benchmarks.Mocks
{
    /// <summary>
    /// Mock de <see cref="IUser"/> para uso em testes de unidade e benchmarks.
    /// Este mock simula o comportamento de um serviço de usuário para fins de teste.
    /// </summary>
    public class UserMock : IUser
    {
        /// <summary>
        /// Obtém o email do usuário simulado.
        /// </summary>
        /// <returns>O email do usuário como uma string.</returns>
        public string GetUserEmail()
        {
            return "user@example.com"; // Retorno simulado
        }

        /// <summary>
        /// Obtém o nome do usuário simulado.
        /// </summary>
        public string Name => "Mock User"; // Nome do usuário simulado

        /// <summary>
        /// Obtém o ID do usuário simulado.
        /// </summary>
        /// <returns>O ID do usuário como um <see cref="Guid"/>.</returns>
        public Guid GetUserId()
        {
            return Guid.NewGuid(); // Retorno de um ID de usuário simulado como Guid
        }

        /// <summary>
        /// Verifica se o usuário simulado está autenticado.
        /// </summary>
        /// <returns>
        /// <c>true</c> se o usuário estiver autenticado; caso contrário, <c>false</c>.
        /// </returns>
        public bool IsAuthenticated()
        {
            return true; // Simulação de usuário autenticado
        }

        /// <summary>
        /// Verifica se o usuário simulado possui um determinado papel.
        /// </summary>
        /// <param name="role">O papel a ser verificado.</param>
        /// <returns>
        /// <c>true</c> se o usuário possuir o papel; caso contrário, <c>false</c>.
        /// </returns>
        public bool IsInRole(string role)
        {
            return false; // Simulação de verificação de papel (sempre falso)
        }

        /// <summary>
        /// Obtém a identidade de claims do usuário simulado.
        /// </summary>
        /// <returns>Uma lista de <see cref="Claim"/> representando a identidade de claims do usuário.</returns>
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return []; // Retorno de uma lista de claims simulada
        }
    }
}