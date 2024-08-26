namespace QualEaResposta.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return string.Empty; // Retorna string vazia ao invés de lançar exceção
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value ?? string.Empty;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return string.Empty; // Retorna string vazia ao invés de lançar exceção
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value ?? string.Empty;
        }
    }
}