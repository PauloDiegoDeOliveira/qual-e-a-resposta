using Microsoft.AspNetCore.Identity;

namespace QualEaResposta.Domain.Model
{
    /// <summary>
    /// Representa um usuário no sistema.
    /// </summary>
    public class Usuario : IdentityUser
    {
        /// <summary>
        /// Obtém ou define o gênero do usuário.
        /// </summary>
        public string Genero { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define se o usuário deseja receber notificações.
        /// </summary>
        public bool Notificar { get; set; }

        /// <summary>
        /// Obtém ou define a versão do token.
        /// </summary>
        public long VersaoToken { get; set; }

        /// <summary>
        /// Obtém ou define o status do usuário.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a data de criação do usuário.
        /// </summary>
        public DateTime CriadoEm { get; set; }

        /// <summary>
        /// Obtém ou define a data da última alteração do usuário.
        /// </summary>
        public DateTime? AlteradoEm { get; set; }

        /// <summary>
        /// Obtém ou define a data e hora do último acesso do usuário.
        /// </summary>
        public DateTimeOffset? UltimoAcesso { get; set; }
    }
}