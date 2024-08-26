namespace QualEaResposta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerguntasController : ControllerBase
    {
        private readonly IPerguntaService _perguntaService;

        public PerguntasController(IPerguntaService perguntaService)
        {
            _perguntaService = perguntaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerguntaById(Guid id)
        {
            var perguntaDTO = await _perguntaService.GetPerguntaByIdAsync(id);
            if (perguntaDTO == null) return NotFound();

            return Ok(perguntaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePergunta([FromBody] PerguntaDTO perguntaDTO)
        {
            if (perguntaDTO == null || string.IsNullOrWhiteSpace(perguntaDTO.TextoPergunta))
            {
                return BadRequest("A pergunta é obrigatória.");
            }

            var alternativasTexto = perguntaDTO.Alternativas.Select(a => a.TextoAlternativa).ToList();
            var createdPerguntaDTO = await _perguntaService.CreatePerguntaAsync(perguntaDTO.TextoPergunta, alternativasTexto);

            return CreatedAtAction(nameof(GetPerguntaById), new { id = createdPerguntaDTO.Id }, createdPerguntaDTO);
        }
    }
}