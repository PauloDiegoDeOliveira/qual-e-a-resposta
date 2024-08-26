namespace QualEaResposta.Infrastructure.Configurations
{
    public abstract class ConfigurationBase<TEntity>(string tableName) : IEntityTypeConfiguration<TEntity> where TEntity : EntidadeBase
    {
        // Propriedade protegida para o nome da tabela
        protected string TableName { get; set; } = string.IsNullOrWhiteSpace(tableName) ? "DefaultTableName" : tableName;

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Configuração da tabela usando o nome fornecido
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id); // Define a chave primária

            builder.Property(p => p.CriadoEm)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()"); // Define o tipo e o valor padrão para a propriedade CriadoEm

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("Status")
                   .HasColumnType("varchar(50)")
                   .HasDefaultValue(EStatus.Ativo.ToString()); // Define o tipo, comprimento máximo e valor padrão para a propriedade Status
        }
    }
}