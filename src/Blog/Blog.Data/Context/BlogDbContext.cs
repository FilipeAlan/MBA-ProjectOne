using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Entidade;

namespace Blog.Data.Context
{
    public class BlogDbContext : IdentityDbContext
    {       
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public BlogDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        }
    }
}
