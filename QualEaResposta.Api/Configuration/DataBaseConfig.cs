namespace QualEaResposta.Api.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));
        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using ApplicationDbContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
    }
}