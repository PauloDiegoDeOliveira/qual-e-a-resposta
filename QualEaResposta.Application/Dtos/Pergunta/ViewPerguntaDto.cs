namespace QualEaResposta.Application.Dtos.Pergunta
{
    /// <summary>
    /// DTO para exibição de uma pergunta, incluindo suas alternativas e informações de estado.
    /// </summary>
    public class ViewPerguntaDto
    {
        /// <summary>
        /// Identificador único da pergunta.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Texto da pergunta.
        /// </summary>
        public string TextoPergunta { get; set; } = string.Empty; // Inicialização para evitar null

        /// <summary>
        /// Lista de alternativas associadas à pergunta.
        /// </summary>
        public List<ViewAlternativaDto> Alternativas { get; set; } = []; // Inicialização correta da lista

        /// <summary>
        /// Status da pergunta (Ativo, Inativo, Pendente, Excluido).
        /// </summary>
        public EStatus Status { get; set; } = EStatus.Ativo; // Usando enum diretamente para segurança e clareza

        /// <summary>
        /// Data e hora de criação da pergunta.
        /// </summary>
        public DateTime CriadoEm { get; set; } // Removido DateTime.Now; será definido automaticamente pelo banco de dados ou em outro lugar apropriado
    }
}