namespace QualEaResposta.Domain.Enums
{
    /// <summary>
    /// Define o status de uma entidade no sistema.
    /// </summary>
    public enum EStatus
    {
        /// <summary>
        /// Indica que a entidade está ativa.
        /// </summary>
        Ativo = 1,

        /// <summary>
        /// Indica que a entidade está inativa.
        /// </summary>
        Inativo,

        /// <summary>
        /// Indica que a entidade está pendente.
        /// </summary>
        Pendente,

        /// <summary>
        /// Indica que a entidade foi excluída.
        /// </summary>
        Excluido
    }
}