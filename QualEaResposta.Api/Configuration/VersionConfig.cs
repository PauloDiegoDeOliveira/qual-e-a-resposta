namespace QualEaResposta.Api.Configuration
{
    public static class VersionConfig
    {
        public static IServiceCollection AddVersionConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                              new HeaderApiVersionReader("x-api-version"),
                                                              new MediaTypeApiVersionReader("x-api-version"));
            }).AddApiExplorer(options =>
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