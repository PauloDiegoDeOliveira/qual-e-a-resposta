namespace QualEaResposta.Application.Mappers
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Pergunta.
    /// </summary>
    public class PerguntaMappingProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PerguntaMappingProfile"/>.
        /// Define os mapeamentos entre a entidade Pergunta e seus DTOs correspondentes.
        /// </summary>
        public PerguntaMappingProfile()
        {
            // Mapeamento de Pergunta para PostPerguntaDto e vice-versa
            CreateMap<Pergunta, PostPerguntaDto>();
            CreateMap<PostPerguntaDto, Pergunta>();

            // Mapeamento de Pergunta para ViewPerguntaDto e vice-versa
            CreateMap<Pergunta, ViewPerguntaDto>();
            CreateMap<ViewPerguntaDto, Pergunta>();
        }
    }
}