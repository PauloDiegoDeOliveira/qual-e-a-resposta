using QualEaResposta.Domain.Model;

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Configura��o do Serilog para logging
    SerilogConfig.AddSerilogApi(builder.Services);
    builder.Host.UseSerilog(Log.Logger);
    Log.Warning("Iniciando API");

    // Configura��es e ambiente da aplica��o
    ConfigurationManager configurationManager = builder.Configuration;
    IWebHostEnvironment environment = builder.Environment;
    Log.Warning("Ambiente atual: {EnvironmentName}", environment.EnvironmentName);

    // Registro de servi�os
    builder.Services.AddControllers();
    builder.Services.AddFluentValidationConfiguration();
    builder.Services.AddAutoMapperConfiguration();
    builder.Services.AddDatabaseConfiguration(configurationManager);
    builder.Services.AddDependencyInjectionConfiguration(configurationManager);
    builder.Services.AddSwaggerConfiguration();
    builder.Services.AddCorsConfiguration(environment);
    builder.Services.AddVersionConfiguration();
    builder.Services.AddHealthChecksConfiguration(configurationManager);
    builder.Services.AddHangfireConfiguration(builder.Configuration);
    builder.Services.AddSignalR();
    builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

    // Constru��o da aplica��o Web
    WebApplication app = builder.Build();
    Log.Warning("Aplica��o constru�da.");

    // Obter o IApiVersionDescriptionProvider para uso na configura��o do Swagger
    IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Configura��o do pipeline de requisi��es HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseCors("Development");
    }
    else
    {
        string corsPolicy = app.Environment.IsStaging() ? "Staging" : "Production";
        app.UseCors(corsPolicy);

        app.UseHttpsRedirection();

        if (app.Environment.IsProduction())
        {
            app.UseHsts();
        }
    }

    // Configura��o do Dashboard do Hangfire
    app.UseHangfireDashboard("/hangfire");
    var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
    HangfireJobsConfig.RegisterJobs(recurringJobManager); // Registra os jobs do Hangfire ap�s o servidor ser inicializado

    // Configura��es adicionais e mapeamento de rotas
    app.UseStaticFiles(); // Middleware para servir arquivos est�ticos
    app.UseRouting(); // Middleware de roteamento deve vir ap�s arquivos est�ticos
    app.UseSwaggerConfiguration(provider); // Passando o provider para configura��o do Swagger
    app.UseDatabaseConfiguration(); // Middleware de configura��o do banco de dados
    app.UseHealthChecksConfiguration(); // Health Checks devem ser configurados antes dos endpoints
    app.MapHub<MessageHub>("/messageHub"); // Mapear o hub do SignalR
    app.MapIdentityApi<Usuario>(); // Mapear a API de identidade com a classe Usuario
    app.MapControllers(); // Mapeia os endpoints do controlador

    // Iniciando a aplica��o
    Log.Warning("API iniciada. Aguardando requisi��es...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Uma exce��o n�o tratada {ExceptionType} ocorreu, levando ao t�rmino da API. Detalhes: {ExceptionMessage}, Pilha de Chamadas: {StackTrace}", ex.GetType().Name, ex.Message, ex.StackTrace);
}
finally
{
    Log.Information("A execu��o da API foi conclu�da �s {DateTime}. Fechando e liberando recursos...", DateTime.Now);
    Log.CloseAndFlush();
}