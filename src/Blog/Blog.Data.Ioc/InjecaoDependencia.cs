using Blog.Data.Context;
using Blog.Data.Entidade;
using Blog.Data.Interface;
using Blog.Data.Repositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Blog.Data.Ioc
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddBlogData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<Autor, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IComentarioRepositorio, ComentarioRepositorio>();
            services.AddScoped<IPostagemRepositorio, PostagemRepositorio>();

            return services;
        }
        public static async Task InicializarDados(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Autor>>(); 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!userManager.Users.Any())
            {
                string[] roles = { "Administrador", "Usuario" };
                foreach (var role in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(role);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var adminUser = new Autor
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Nome = "Administrador"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(adminUser, new Claim("Nome", adminUser.Nome));
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
        }
    }
}
