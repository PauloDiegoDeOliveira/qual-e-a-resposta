namespace QualEaResposta.Infrastructure.Configurations
{
    public class PerguntaConfiguration : IEntityTypeConfiguration<Pergunta>
    {
        public void Configure(EntityTypeBuilder<Pergunta> builder)
        {
            // Configura o nome da tabela
            builder.ToTable("Perguntas");

            // Configura a chave primária
            builder.HasKey(p => p.Id);

            // Configura o campo TextoPergunta
            builder.Property(p => p.TextoPergunta)
                   .IsRequired()
                   .HasMaxLength(200);

            // Configura o relacionamento com a entidade Alternativa
            builder.HasMany(p => p.Alternativas)
                   .WithOne(a => a.Pergunta)
                   .HasForeignKey(a => a.PerguntaId);
        }
    }
}