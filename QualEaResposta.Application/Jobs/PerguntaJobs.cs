namespace QualEaResposta.Application.Jobs
{
    /// <summary>
    /// Classe contendo métodos para execução de jobs de fundo relacionados a perguntas.
    /// </summary>
    public class PerguntaJobs
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PerguntaJobs"/>.
        /// </summary>
        public PerguntaJobs()
        {
            // Construtor sem dependências, já que estamos apenas registrando a data e hora
        }

        /// <summary>
        /// Job para registrar a data e hora atuais.
        /// </summary>
        public static async Task RegistrarDataHoraAsync()
        {
            DateTime dataHoraAtual = DateTime.Now;
            Console.WriteLine($"Job executado em {dataHoraAtual:yyyy-MM-dd HH:mm:ss}");
            await Task.CompletedTask;
        }
    }
}