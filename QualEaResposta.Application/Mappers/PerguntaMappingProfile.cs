namespace QualEaResposta.Application.Mappers
{
    public class PerguntaMappingProfile : Profile
    {
        public PerguntaMappingProfile()
        {
            CreateMap<Pergunta, PerguntaDTO>();
            CreateMap<PerguntaDTO, Pergunta>();
        }
    }
}