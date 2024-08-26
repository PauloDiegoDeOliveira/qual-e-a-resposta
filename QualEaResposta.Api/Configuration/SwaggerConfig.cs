namespace QualEaResposta.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                // Adicionando comentários de XML para documentação do Swagger
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(AppContext.BaseDirectory, "QualEaResposta.Application.xml");
                c.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(AppContext.BaseDirectory, "QualEaResposta.Domain.xml");
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (string description in provider.ApiVersionDescriptions.Select(x => x.GroupName))
                    {
                        options.SwaggerEndpoint($"/swagger/{description}/swagger.json", description.ToUpperInvariant());
                    }
                });

            return app;
        }
    }

    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Qual é a Resposta API",
                Version = description.ApiVersion.ToString(),
                Description = "API do projeto \"Qual é a Resposta\" que consome o ChatGPT para obter respostas a partir das perguntas enviadas.",
                Contact = new OpenApiContact
                {
                    Name = "Paulo Diego de Oliveira",
                    Email = "pdiegodo@gmail.com",
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                parameter.Description ??= description.ModelMetadata?.Description;

                if (routeInfo is null)
                {
                    continue;
                }

                if (parameter.In != ParameterLocation.Path && parameter.Schema.Default is null)
                {
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue?.ToString());
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}