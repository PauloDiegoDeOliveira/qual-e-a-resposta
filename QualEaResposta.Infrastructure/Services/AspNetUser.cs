namespace QualEaResposta.Infrastructure.Services
{
    public class AspNetUser(IHttpContextAccessor accessor) : IUser
    {
        private readonly IHttpContextAccessor accessor = accessor ?? new HttpContextAccessor();

        public string Name => accessor.HttpContext?.User.Identity?.Name ?? string.Empty;

        public Guid GetUserId()
        {
            // Verifica se o HttpContext e o User não são nulos antes de desreferenciar
            if (IsAuthenticated())
            {
                var userId = accessor.HttpContext?.User?.GetUserId();
                return Guid.TryParse(userId, out var guid) ? guid : Guid.Empty;
            }
            return Guid.Empty;
        }

        public string GetUserEmail()
        {
            // Verifica se o HttpContext e o User não são nulos antes de desreferenciar
            return IsAuthenticated() ? accessor.HttpContext?.User?.GetUserEmail() ?? string.Empty : string.Empty;
        }

        public bool IsAuthenticated()
        {
            return accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }

        public bool IsInRole(string role)
        {
            return accessor.HttpContext?.User.IsInRole(role) ?? false;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return accessor.HttpContext?.User?.Claims ?? []; // Retorna uma lista vazia de claims
        }

        IEnumerable<Claim> IUser.GetClaimsIdentity()
        {
            return []; // Retorna uma lista vazia ao invés de lançar uma exceção
        }
    }
}