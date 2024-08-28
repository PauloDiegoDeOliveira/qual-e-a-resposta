namespace QualEaResposta.Benchmarks.Mocks
{
    /// <summary>
    /// Mock de <see cref="INotificationService"/> para uso em testes de unidade e benchmarks.
    /// Este mock simula o comportamento do serviço de notificações para fins de teste.
    /// </summary>
    public class NotificationServiceMock : INotificationService
    {
        /// <summary>
        /// Simula a notificação de uma mensagem genérica.
        /// </summary>
        /// <param name="mensagem">A mensagem a ser notificada.</param>
        public void Notificar(string mensagem)
        {
            // Implementação simulada
        }

        /// <summary>
        /// Verifica se há alguma notificação pendente.
        /// </summary>
        /// <returns>
        /// <c>true</c> se houver notificações pendentes; caso contrário, <c>false</c>.
        /// </returns>
        public bool TemNotificacao()
        {
            return false; // Simulação de retorno
        }

        /// <summary>
        /// Obtém a lista de notificações pendentes.
        /// </summary>
        /// <returns>Uma lista de mensagens de notificação.</returns>
        public List<string> ObterNotificacoes()
        {
            return new List<string>(); // Simulação de retorno
        }

        /// <summary>
        /// Simula a notificação de uma mensagem de erro.
        /// </summary>
        /// <param name="mensagem">A mensagem de erro a ser notificada.</param>
        public void NotificarErro(string mensagem)
        {
            // Implementação simulada para notificar erro
        }

        /// <summary>
        /// Simula a notificação de uma mensagem informativa.
        /// </summary>
        /// <param name="mensagem">A mensagem informativa a ser notificada.</param>
        public void NotificarMensagem(string mensagem)
        {
            // Implementação simulada para notificar mensagem
        }

        /// <summary>
        /// Verifica se a operação atual é válida.
        /// </summary>
        /// <returns>
        /// <c>true</c> se a operação for válida; caso contrário, <c>false</c>.
        /// </returns>
        public bool OperacaoValida()
        {
            return true; // Simulação de que a operação é válida
        }

        /// <summary>
        /// Obtém todas as mensagens notificadas.
        /// </summary>
        /// <returns>Uma lista de todas as mensagens notificadas.</returns>
        public List<string> ObterMensagens()
        {
            return []; // Simulação de retorno de mensagens
        }
    }
}