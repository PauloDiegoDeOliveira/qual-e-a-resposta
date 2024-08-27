namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para o Dashboard do Hangfire.
    /// </summary>
    public static class HangfireDashboardConfig
    {
        /// <summary>
        /// Configura o Dashboard do Hangfire para a aplicação.
        /// </summary>
        /// <param name="app">A aplicação Web.</param>
        /// <param name="configuration">A configuração da aplicação para obter as credenciais do Dashboard.</param>
        public static void UseHangfireDashboardWithConfig(this WebApplication app, IConfiguration configuration)
        {
            // Obtendo as credenciais de configuração ou definindo um valor padrão se nulo
            string user = configuration.GetValue<string>("HangfireSettings:UserName") ?? string.Empty;
            string pass = configuration.GetValue<string>("HangfireSettings:Password") ?? string.Empty;

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization =
                [
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = user,
                        Pass = pass
                    }
                ]
            });
        }
    }
}