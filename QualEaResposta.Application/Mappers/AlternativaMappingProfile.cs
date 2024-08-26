namespace QualEaResposta.Application.Mappers
{
    public class AlternativaMappingProfile : Profile
    {
        public AlternativaMappingProfile()
        {
            CreateMap<Alternativa, AlternativaDTO>();
            CreateMap<AlternativaDTO, Alternativa>();
        }
    }
}