using Blog.Data.Context;
using Blog.Data.Interface;
using Blog.Data.Repositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Data.Ioc
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddBlogData(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BlogDbContext>();            
            services.AddScoped<IAutorRepositorio, AutorRepositorio>();
            services.AddScoped<IComentarioRepositorio, ComentarioRepositorio>();
            services.AddScoped<IPostagemRepositorio, PostagemRepositorio>();            

            return services;
        }
    }
}
