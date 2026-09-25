using BibliotecaMVC.Repositories;
using BibliotecaMVC.Data;
using BibliotecaMVC.Services;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
