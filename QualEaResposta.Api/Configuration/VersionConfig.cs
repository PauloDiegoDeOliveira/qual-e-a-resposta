namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Configuração de versão da API para o projeto QualEaResposta.
    /// </summary>
    public static class VersionConfig
    {
        /// <summary>
        /// Adiciona configurações de versionamento de API à coleção de serviços.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <returns>A coleção de serviços modificada com as configurações de versionamento de API.</returns>
        public static IServiceCollection AddVersionConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version")
                );
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}