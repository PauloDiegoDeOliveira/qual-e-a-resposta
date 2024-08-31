namespace QualEaResposta.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica automaticamente todas as configurações de entidade do assembly atual
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Itera sobre todas as entidades no modelo
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Verifica se a entidade é do tipo base "EntidadeBase"
                if (typeof(EntidadeBase).IsAssignableFrom(entityType.ClrType))
                {
                    // Aplica um filtro global para entidades que derivam de "EntidadeBase"
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateFilterExpression(entityType.ClrType));
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        // Método para criar expressão de filtro
        private static LambdaExpression CreateFilterExpression(Type type)
        {
            ParameterExpression lambdaParam = Expression.Parameter(type, "e");

            // Conversão da propriedade Status do tipo enum para string para comparação
            MemberExpression statusProperty = Expression.Property(lambdaParam, nameof(EntidadeBase.Status));
            MethodCallExpression statusToString = Expression.Call(statusProperty, nameof(Enum.ToString), null);

            BinaryExpression lambdaBody = Expression.NotEqual(
                statusToString,
                Expression.Constant(EStatus.Excluido.ToString()));

            return Expression.Lambda(lambdaBody, lambdaParam);
        }
    }
}