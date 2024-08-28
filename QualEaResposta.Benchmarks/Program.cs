namespace QualEaResposta.Benchmarks
{
    /// <summary>
    /// Ponto de entrada para a execução de benchmarks.
    /// Configura e executa os benchmarks definidos no projeto usando o BenchmarkDotNet.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Método principal que inicia a execução dos benchmarks.
        /// </summary>
        /// <param name="_">Argumentos de linha de comando (não utilizados).</param>
        public static void Main(string[] _)
        {
            // Executa os benchmarks definidos na classe VersaoControllerBenchmark
            BenchmarkRunner.Run<VersaoControllerBenchmark>(); // Executa o benchmark sem atribuição
        }
    }
}