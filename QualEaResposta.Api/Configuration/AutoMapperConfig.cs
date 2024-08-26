namespace QualEaResposta.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PerguntaMappingProfile),
                                   typeof(AlternativaMappingProfile));
        }
    }
}