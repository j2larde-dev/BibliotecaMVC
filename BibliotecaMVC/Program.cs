using BibliotecaMVC.Repositories;
using BibliotecaMVC.Data;
using BibliotecaMVC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Servicio del módulo Autores (Scoped)
builder.Services.AddScoped<IAutorService, AutorServiceAlternativo>();

// Servicio del módulo Libros (Singleton)
builder.Services.AddSingleton<IRepositorioLibro, RepositorioEnMemoria>();

// DbContext
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BibliotecaDB"))
    );

// Identity: usuarios + roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<BibliotecaContext>();

// Configuración de cookie de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();    //Habilita manejo de cookies y sesiones
app.UseAuthorization();     //Habilita evaluación de [Authorize] y roles

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


Task.Run(async () =>
{
    await SeedRolesAndUsersAsync(app);
}).Wait();

app.Run();

// Inicialización (seeding) de roles.
static async Task SeedRolesAndUsersAsync(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = new[] { "Admin", "Bibliotecario", "Usuario" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Usuario AdminLógico: JuanLara
    var adminUser = await userManager.FindByNameAsync("JuanLara");
    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // Usuario llamado literalmente "Admin"
    var adminLoginUser = await userManager.FindByNameAsync("Admin");
    if (adminLoginUser == null)
    {
        adminLoginUser = new IdentityUser
        {
            UserName = "Admin",
            Email = "admin@biblioteca.local",
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(adminLoginUser, "Admin123!");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminLoginUser, "Admin");
        }
    }
}



























