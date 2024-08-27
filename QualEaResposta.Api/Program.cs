try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Configuração do Serilog para logging
    SerilogConfig.AddSerilogApi(builder.Services);
    builder.Host.UseSerilog(Log.Logger);
    Log.Warning("Iniciando API");

    // Configurações e ambiente da aplicação
    ConfigurationManager configurationManager = builder.Configuration;
    IWebHostEnvironment environment = builder.Environment;
    Log.Warning("Ambiente atual: {EnvironmentName}", environment.EnvironmentName);

    // Registro de serviços
    builder.Services.AddControllers();
    builder.Services.AddFluentValidationConfiguration();
    builder.Services.AddAutoMapperConfiguration();
    builder.Services.AddDatabaseConfiguration(configurationManager);
    builder.Services.AddDependencyInjectionConfiguration(configurationManager);
    builder.Services.AddSwaggerConfiguration();
    builder.Services.AddCorsConfiguration(environment);
    builder.Services.AddVersionConfiguration();
    builder.Services.AddHealthChecksConfiguration(configurationManager);
    builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

    // Construção da aplicação Web
    WebApplication app = builder.Build();
    Log.Warning("Aplicação construída.");

    // Obter o IApiVersionDescriptionProvider para uso na configuração do Swagger
    IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Configuração do pipeline de requisições HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseCors("Development");
    }
    else
    {
        app.UseCors(app.Environment.IsStaging() ? "Staging" : "Production");

        if (app.Environment.IsProduction())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
    }

    // Configurações adicionais e mapeamento de rotas
    app.UseStaticFiles(); // Middleware para servir arquivos estáticos
    app.UseRouting(); // Middleware de roteamento deve vir após arquivos estáticos
    app.UseSwaggerConfiguration(provider); // Passando o provider para configuração do Swagger
    app.UseDatabaseConfiguration(); // Middleware de configuração do banco de dados
    app.UseHealthChecksConfiguration(); // Health Checks devem ser configurados antes dos endpoints
    app.MapControllers(); // Mapeia os endpoints do controlador

    // Iniciando a aplicação
    Log.Warning("API iniciada. Aguardando requisições...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Uma exceção não tratada {ExceptionType} ocorreu, levando ao término da API. Detalhes: {ExceptionMessage}, Pilha de Chamadas: {StackTrace}", ex.GetType().Name, ex.Message, ex.StackTrace);
}
finally
{
    Log.Information("A execução da API foi concluída às {DateTime}. Fechando e liberando recursos...", DateTime.Now);
    Log.CloseAndFlush();
}