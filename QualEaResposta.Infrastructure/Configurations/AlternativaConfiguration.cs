namespace QualEaResposta.Infrastructure.Configurations
{
    public class AlternativaConfiguration : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            // Configura o nome da tabela
            builder.ToTable("Alternativas");

            // Configura a chave primária
            builder.HasKey(a => a.Id);

            // Configura o campo TextoAlternativa
            builder.Property(a => a.TextoAlternativa)
                   .IsRequired()
                   .HasMaxLength(100);

            // Configura o relacionamento com a entidade Pergunta
            builder.HasOne(a => a.Pergunta)
                   .WithMany(p => p.Alternativas)
                   .HasForeignKey(a => a.PerguntaId)
                   .OnDelete(DeleteBehavior.Cascade); // Define o comportamento de exclusão em cascata
        }
    }
}