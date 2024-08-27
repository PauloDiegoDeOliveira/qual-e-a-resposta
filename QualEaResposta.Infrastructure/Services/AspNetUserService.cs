namespace QualEaResposta.Infrastructure.Services
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="AspNetUserService"/>.
    /// </summary>
    /// <param name="accessor">Acessor do contexto HTTP.</param>
    /// <param name="logger">Logger para registrar eventos e erros.</param>
    public class AspNetUserService(IHttpContextAccessor accessor, ILogger<AspNetUserService> logger) : IUser
    {
        private readonly IHttpContextAccessor _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        private readonly ILogger<AspNetUserService> _logger = logger;

        public string Name => _accessor.HttpContext?.User.Identity?.Name ?? string.Empty;

        public Guid GetUserId()
        {
            if (IsAuthenticated())
            {
                var userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userId, out var guid))
                {
                    _logger.LogInformation("Usuário autenticado com ID {UserId}", guid);
                    return guid;
                }
                else
                {
                    _logger.LogWarning("Falha ao converter o ID do usuário para GUID. UserId: {UserId}", userId);
                }
            }
            else
            {
                _logger.LogWarning("Tentativa de obter ID do usuário quando não está autenticado.");
            }
            return Guid.Empty;
        }

        public string GetUserEmail()
        {
            if (IsAuthenticated())
            {
                var email = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
                _logger.LogInformation("E-mail do usuário autenticado: {UserEmail}", email);
                return email;
            }

            _logger.LogWarning("Tentativa de obter e-mail do usuário quando não está autenticado.");
            return string.Empty;
        }

        public bool IsAuthenticated()
        {
            var isAuthenticated = _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
            _logger.LogInformation("Verificação de autenticação do usuário: {IsAuthenticated}", isAuthenticated);
            return isAuthenticated;
        }

        public bool IsInRole(string role)
        {
            var isInRole = _accessor.HttpContext?.User.IsInRole(role) ?? false;
            _logger.LogInformation("Verificação de papel do usuário: {Role}, Resultado: {IsInRole}", role, isInRole);
            return isInRole;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            var claims = _accessor.HttpContext?.User?.Claims ?? Enumerable.Empty<Claim>();
            _logger.LogInformation("Obtenção de Claims do usuário. Total Claims: {ClaimsCount}", claims.Count());
            return claims;
        }

        IEnumerable<Claim> IUser.GetClaimsIdentity()
        {
            _logger.LogInformation("Obtenção de Claims do usuário como IUser. Nenhuma Claim retornada.");
            return Enumerable.Empty<Claim>();
        }
    }
}