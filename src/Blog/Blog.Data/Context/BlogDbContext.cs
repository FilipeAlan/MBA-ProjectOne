using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Entidade;

namespace Blog.Data.Context
{
    public class BlogDbContext : IdentityDbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<Autor> Autores { get; set; }
        DbSet<Comentario> Comentarios { get; set; }
        DbSet<Postagem> Postagens { get; set; }
    }
}
