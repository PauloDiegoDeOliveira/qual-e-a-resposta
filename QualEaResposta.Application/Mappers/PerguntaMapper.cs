//namespace QualEaResposta.Application.Mappers
//{
//    public class PerguntaMapper : IMapper<Pergunta, PerguntaDTO>, IMapper<PerguntaDTO, Pergunta>
//    {
//        public PerguntaDTO Map(Pergunta source)
//        {
//            return new PerguntaDTO
//            {
//                Id = source.Id,
//                TextoPergunta = source.TextoPergunta,
//                Alternativas = source.Alternativas?.Select(a => new AlternativaDTO
//                {
//                    Id = a.Id,
//                    TextoAlternativa = a.TextoAlternativa,
//                    ECorreta = a.ECorreta
//                }).ToList()
//            };
//        }

//        public Pergunta Map(PerguntaDTO source)
//        {
//            return new Pergunta
//            {
//                Id = source.Id,
//                TextoPergunta = source.TextoPergunta,
//                Alternativas = source.Alternativas?.Select(a => new Alternativa
//                {
//                    Id = a.Id,
//                    TextoAlternativa = a.TextoAlternativa,
//                    ECorreta = a.ECorreta
//                }).ToList()
//            };
//        }

//        public IEnumerable<PerguntaDTO> Map(IEnumerable<Pergunta> source)
//        {
//            foreach (var item in source)
//            {
//                yield return Map(item);
//            }
//        }

//        public IEnumerable<Pergunta> Map(IEnumerable<PerguntaDTO> source)
//        {
//            foreach (var item in source)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}