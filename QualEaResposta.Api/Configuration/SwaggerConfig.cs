namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para o Swagger.
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Configura o Swagger para a aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <returns>A coleção de serviços modificada.</returns>
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

        /// <summary>
        /// Configura o uso do Swagger na aplicação.
        /// </summary>
        /// <param name="app">A aplicação web.</param>
        /// <param name="provider">O provedor de descrição da versão da API.</param>
        /// <returns>A aplicação web modificada.</returns>
        public static WebApplication UseSwaggerConfiguration(this WebApplication app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions.Select(x => x.GroupName))
                    {
                        options.SwaggerEndpoint($"/swagger/{description}/swagger.json", description.ToUpperInvariant());
                    }
                });

            return app;
        }
    }

    /// <summary>
    /// Configuração personalizada para o Swagger para lidar com múltiplas versões da API.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="ConfigureSwaggerOptions"/>.
    /// </remarks>
    /// <param name="provider">O provedor de descrição da versão da API.</param>
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider = provider;

        /// <summary>
        /// Configura as opções do Swagger.
        /// </summary>
        /// <param name="options">As opções de configuração do Swagger.</param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Cria as informações para uma versão específica da API.
        /// </summary>
        /// <param name="description">A descrição da versão da API.</param>
        /// <returns>As informações da versão da API.</returns>
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

    /// <summary>
    /// Filtro padrão do Swagger para adicionar valores padrão às operações.
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Aplica o filtro às operações Swagger.
        /// </summary>
        /// <param name="operation">A operação Swagger.</param>
        /// <param name="context">O contexto do filtro da operação.</param>
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