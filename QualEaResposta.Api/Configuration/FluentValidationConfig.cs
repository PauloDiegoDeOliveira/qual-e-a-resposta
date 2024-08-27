namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Configuração para FluentValidation.
    /// </summary>
    public static class FluentValidationConfig
    {
        /// <summary>
        /// Adiciona a configuração do FluentValidation ao serviço de injeção de dependências.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            ConfigureControllers(services);
            RegisterValidators(services);
            ConfigureGlobalValidationOptions();
        }

        /// <summary>
        /// Configura as opções de serialização e enumeração para os controladores.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        private static void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }

        /// <summary>
        /// Configura as opções globais para o FluentValidation, como a cultura e o resolver de nomes.
        /// </summary>
        private static void ConfigureGlobalValidationOptions()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            // Configura o nome para mensagens de validação usando o atributo Display ou o nome da propriedade.
            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                if (memberInfo == null)
                {
                    return string.Empty;
                }

                DisplayAttribute? displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>(inherit: false);
                return displayAttribute?.GetName() ?? memberInfo.Name;
            };
        }

        /// <summary>
        /// Registra os validadores utilizados na aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        private static void RegisterValidators(IServiceCollection services)
        {
            // Registro automático de todos os validadores presentes no assembly
            //services.AddValidatorsFromAssemblyContaining<PostPerguntaDtoValidator>();
            //services.AddValidatorsFromAssemblyContaining<PutPerguntaDtoValidator>();

            //services.AddValidatorsFromAssemblyContaining<PostAlternativaDtoValidator>();
            //services.AddValidatorsFromAssemblyContaining<PutAlternativaDtoValidator>();

            // Configuração global do FluentValidation
            services.AddFluentValidationAutoValidation();
        }
    }
}