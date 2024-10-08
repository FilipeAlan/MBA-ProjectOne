using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Data.Entidade;

namespace Blog.Data.Configuracao
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("AUTOR");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(a => a.Postagens)
                .WithOne(p => p.Autor)
                .HasForeignKey(p => p.AutorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AUTOR_POSTAGEM");
        }
    }
}
