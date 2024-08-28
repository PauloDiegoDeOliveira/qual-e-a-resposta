namespace QualEaResposta.Api.Hubs
{
    /// <summary>
    /// Hub para gerenciar a comunicação em tempo real via SignalR.
    /// </summary>
    public class MessageHub : Hub
    {
        /// <summary>
        /// Envia uma mensagem para todos os clientes conectados.
        /// </summary>
        /// <param name="message">A mensagem a ser enviada para os clientes.</param>
        /// <returns>Uma tarefa assíncrona representando a operação de envio.</returns>
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}