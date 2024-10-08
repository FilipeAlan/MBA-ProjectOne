using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Data.Entidade;

namespace Blog.Data.Configuracao
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("COMENTARIO");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Conteudo)
                .HasColumnName("CONTEUDO")
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(c => c.Postagem)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.PostagemId)                
                .HasConstraintName("FK_COMENTARIO_POSTAGEM");
        }
    }
}
