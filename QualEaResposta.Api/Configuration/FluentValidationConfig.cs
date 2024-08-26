namespace QualEaResposta.Api.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            ConfigureControllers(services);
            RegisterValidators(services);
            ConfigureGlobalValidationOptions();
        }

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

        private static void ConfigureGlobalValidationOptions()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            // Configura o nome para mensagens de validação, usando como exemplo: [Display(Name = "Paulo Diego")] ou o nome da propriedade.
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

        private static void RegisterValidators(IServiceCollection services)
        {
            // Perguntas
            //services.AddValidatorsFromAssemblyContaining<PostPerguntaValidator>();
            //services.AddValidatorsFromAssemblyContaining<PutPerguntaValidator>();

            // Alternativas
            //services.AddValidatorsFromAssemblyContaining<PostAlternativaValidator>();
            //services.AddValidatorsFromAssemblyContaining<PutAlternativaValidator>();

            // Configuração global do FluentValidation
            services.AddFluentValidationAutoValidation();
        }
    }
}