namespace QualEaResposta.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pergunta>()
                .HasMany(p => p.Alternativas)
                .WithOne(a => a.Pergunta)
                .HasForeignKey(a => a.PerguntaId);
        }
    }
}