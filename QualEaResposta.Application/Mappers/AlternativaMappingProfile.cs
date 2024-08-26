namespace QualEaResposta.Application.Mappers
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Alternativa.
    /// </summary>
    public class AlternativaMappingProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AlternativaMappingProfile"/>.
        /// Define os mapeamentos entre a entidade Alternativa e seus DTOs correspondentes.
        /// </summary>
        public AlternativaMappingProfile()
        {
            // Mapeamento de Alternativa para PostAlternativaDto e vice-versa
            CreateMap<Alternativa, PostAlternativaDto>();
            CreateMap<PostAlternativaDto, Alternativa>();

            // Mapeamento de Alternativa para ViewAlternativaDto e vice-versa
            CreateMap<Alternativa, ViewAlternativaDto>();
            CreateMap<ViewAlternativaDto, Alternativa>();
        }
    }
}