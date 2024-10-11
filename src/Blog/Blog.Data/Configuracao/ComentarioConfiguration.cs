using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Data.Entidade;

namespace Blog.Data.Configuracao
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentario");

            builder.HasKey(c => c.Id);     

            builder.Property(c => c.Nome)                
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)                
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.DataPublicacao)
                .IsRequired();

            builder.Property(c => c.Conteudo)                
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(c => c.Postagem)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.PostagemId);
            
            builder.HasOne(c => c.Autor)
                .WithMany() 
                .HasForeignKey(c => c.AutorId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
