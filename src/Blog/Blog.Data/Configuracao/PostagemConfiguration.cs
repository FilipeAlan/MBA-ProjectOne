using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Data.Entidade;

namespace Blog.Data.Configuracao
{
    public class PostagemConfiguration : IEntityTypeConfiguration<Postagem>
    {
        public void Configure(EntityTypeBuilder<Postagem> builder)
        {
            builder.ToTable("POSTAGEM");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataPublicacao)
                .HasColumnName("DATA_PUBLICACAO")
                .IsRequired();

            builder.Property(p => p.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Conteudo)
                .HasColumnName("CONTEUDO")
                .HasMaxLength(500)
                .IsRequired();

            builder.HasMany(p => p.Comentarios)
                .WithOne(c => c.Postagem)
                .HasForeignKey(c => c.PostagemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_POSTAGEM_COMENTARIO");
        }
    }
}
