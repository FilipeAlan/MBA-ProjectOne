using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Data.Entidade;

namespace Blog.Data.Configuracao
{
    public class PostagemConfiguration : IEntityTypeConfiguration<Postagem>
    {
        public void Configure(EntityTypeBuilder<Postagem> builder)
        {           
            builder.ToTable("Postagem");

            builder.HasKey(p => p.Id);        

            builder.Property(p => p.Titulo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Conteudo)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(p => p.DataPublicacao)
                .IsRequired();
            
            builder.HasOne(p => p.Autor)
                .WithMany(p => p.Postagens)  
                .HasForeignKey(p => p.AutorId)             
                .OnDelete(DeleteBehavior.Cascade);  
            
            builder.HasMany(p => p.Comentarios)
                .WithOne(p => p.Postagem)
                .HasForeignKey(p =>p.PostagemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
