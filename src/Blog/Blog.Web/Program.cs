using Blog.Data.Ioc;
using Blog.Web.Mapping;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddBlogData(builder.Configuration);
builder.Services.AddAutoMapper(typeof(EntidadeModelMapping));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Autor/Login";  
    options.AccessDeniedPath = "/Autor/AccessDenied";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await InjecaoDependencia.InicializarDados(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
