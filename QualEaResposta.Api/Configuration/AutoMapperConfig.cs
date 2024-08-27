namespace QualEaResposta.Api.Configuration
{
    /// <summary>
    /// Classe de configuração para o AutoMapper.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Adiciona a configuração do AutoMapper aos serviços do aplicativo.
        /// </summary>
        /// <param name="services">A coleção de serviços do aplicativo.</param>
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PerguntaMappingProfile),
                                   typeof(AlternativaMappingProfile));
        }
    }
}