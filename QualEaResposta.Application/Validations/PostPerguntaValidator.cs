namespace QualEaResposta.Application.Validations
{
    /// <summary>
    /// Validador para o DTO de criação de pergunta.
    /// </summary>
    public class PostPerguntaValidator : AbstractValidator<PostPerguntaDto>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PostPerguntaValidator"/>.
        /// </summary>
        public PostPerguntaValidator()
        {
            RuleFor(x => x.TextoPergunta)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} não pode ser nulo ou vazio.")
                .Length(1, 200)
                .WithMessage("O campo {PropertyName} deve ter entre 1 e 200 caracteres.");

            // Validação condicional: aplica-se apenas se a lista de alternativas não for nula e não estiver vazia
            When(x => x.Alternativas != null && x.Alternativas.Count != 0, () =>
            {
                RuleFor(x => x.Alternativas)
                    .Must(a => a.Count > 1)
                    .WithMessage("Deve haver pelo menos duas alternativas.");

                RuleForEach(x => x.Alternativas)
                    .ChildRules(alternativa =>
                    {
                        alternativa.RuleFor(a => a.TextoAlternativa)
                            .NotEmpty()
                            .WithMessage("O campo {PropertyName} de cada alternativa não pode ser nulo ou vazio.");
                    });
            });
        }
    }
}