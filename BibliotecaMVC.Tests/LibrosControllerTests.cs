using BibliotecaMVC.Controllers;
using BibliotecaMVC.Data;
using BibliotecaMVC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

namespace BibliotecaMVC.Tests;

public class LibrosControllerTests
{
    private static LibrosController CreateController(
        BibliotecaContext context)
    {
        var logger =
            new Mock<ILogger<LibrosController>>();

        return new LibrosController(
            context,
            logger.Object
        );
    }

    private static BibliotecaContext CreateContext(
        string? databaseName = null)
    {
        var options =
            new DbContextOptionsBuilder<BibliotecaContext>()
                .UseInMemoryDatabase(
                    databaseName ?? Guid.NewGuid().ToString()
                )
                .Options;

        return new BibliotecaContext(options);
    }

    [Fact]
    public async Task Index_DevuelveLaListaDeLibros()
    {
        using var context = CreateContext();

        context.Libros.AddRange(
            new Libro
            {
                Titulo = "ASP.NET Core",
                Autor = "Juan Pérez",
                Categoria = "Programación",
                Precio = 25,
                Disponible = true
            },
            new Libro
            {
                Titulo = "Entity Framework",
                Autor = "María López",
                Categoria = "Bases de Datos",
                Precio = 30,
                Disponible = true
            });

        await context.SaveChangesAsync();

        var controller = CreateController(context);

        var result = await controller.Index();

        var viewResult =
            Assert.IsType<ViewResult>(result);

        var model =
            Assert.IsAssignableFrom<IEnumerable<Libro>>
            (viewResult.Model);

        Assert.Equal(2, model.Count());
    }


    [Fact]
    public async Task Details_ConLibroExistente_DevuelveElLibro()
    {
        using var context = CreateContext();

        var libro = new Libro
        {
            Titulo = "Clean Code",
            Autor = "Robert Martin",
            Categoria = "Programación",
            Precio = 45,
            Disponible = true
        };

        context.Libros.Add(libro);

        await context.SaveChangesAsync();

        var controller = CreateController(context);

        var result = await controller.Details(libro.ID);

        var viewResult =
            Assert.IsType<ViewResult>(result);

        var libroModel =
            Assert.IsType<Libro>(
                viewResult.Model);

        Assert.Equal(
            "Clean Code",
            libroModel.Titulo);
    }


    [Fact]
    public async Task Details_ConLibroInexistente_DevuelveNotFound()
    {
        using var context = CreateContext();

        var controller = CreateController(context);

        var result =
            await controller.Details(999);

        Assert.IsType<NotFoundResult>(
            result);
    }

    [Fact]
    public async Task Create_ConLibroValido_GuardaLibroYRedirigeAlIndice()
    {
        using var context = CreateContext();

        var controller = CreateController(context);

        var libro = new Libro
        {
            Titulo = "Patrones de Diseño",
            Autor = "Gamma",
            Categoria = "Programación",
            Precio = 40,
            Disponible = true
        };

        var result = await controller.Create(libro);

        var redirectResult =
            Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal(
            nameof(LibrosController.Index),
            redirectResult.ActionName);

        Assert.Equal(
            1,
            await context.Libros.CountAsync());
    }

    [Fact]
    public async Task Create_ConModeloInvalido_NoGuardaYDevuelveVista()
    {
        using var context = CreateContext();

        var controller = CreateController(context);

        controller.ModelState.AddModelError(
            "Titulo",
            "El título es obligatorio");

        var libro = new Libro
        {
            Autor = "Autor Prueba"
        };

        var result =
            await controller.Create(libro);

        Assert.IsType<ViewResult>(result);

        Assert.Empty(context.Libros);
    }

    [Fact]
    public async Task Edit_ConLibroValido_ActualizaLibro()
    {
        using var context = CreateContext();

        var libro = new Libro
        {
            Titulo = "Libro Original",
            Autor = "Autor Original",
            Categoria = "Prueba",
            Precio = 10,
            Disponible = true
        };

        context.Libros.Add(libro);
        await context.SaveChangesAsync();

        var controller = CreateController(context);

        libro.Titulo = "Libro Actualizado";

        var result =
            await controller.Edit(
                libro.ID,
                libro);

        var redirectResult =
            Assert.IsType<RedirectToActionResult>(
                result);

        Assert.Equal(
            nameof(LibrosController.Index),
            redirectResult.ActionName);

        var libroActualizado =
            await context.Libros.FindAsync(libro.ID);

        Assert.NotNull(libroActualizado);

        Assert.Equal(
            "Libro Actualizado",
            libroActualizado!.Titulo);
    }

    [Fact]
    public async Task Edit_ConIdentificadorDiferente_DevuelveBadRequest()
    {
        using var context = CreateContext();

        var controller = CreateController(context);

        var libro = new Libro
        {
            ID = 1,
            Titulo = "Libro Prueba",
            Autor = "Autor",
            Categoria = "Prueba"
        };

        var result =
            await controller.Edit(
                2,
                libro);

        Assert.IsType<BadRequestResult>(
            result);
    }

    [Fact]
    public async Task Edit_ConLibroInexistente_DevuelveNotFound()
    {
        using var context = CreateContext();

        var controller = CreateController(context);

        var libro = new Libro
        {
            ID = 999,
            Titulo = "Libro Inexistente",
            Autor = "Autor",
            Categoria = "Prueba"
        };

        var result =
            await controller.Edit(
                999,
                libro);

        Assert.IsType<NotFoundResult>(
            result);
    }

    [Fact]
    public async Task Delete_ConLibroExistente_EliminaLibroYRedirigeAlIndice()
    {
        using var context = CreateContext();

        var libro = new Libro
        {
            Titulo = "Libro para eliminar",
            Autor = "Autor",
            Categoria = "Prueba",
            Precio = 15,
            Disponible = true
        };

        context.Libros.Add(libro);

        await context.SaveChangesAsync();

        var controller = CreateController(context);

        var result =
            await controller.Delete(libro.ID);

        var redirectResult =
            Assert.IsType<RedirectToActionResult>(
                result);

        Assert.Equal(
            nameof(LibrosController.Index),
            redirectResult.ActionName);

        Assert.Empty(context.Libros);
    }

    [Fact]
    public async Task Delete_ConLibroInexistente_DevuelveNotFound()
    {
        using var context = CreateContext();

        var controller = CreateController(context);

        var result =
            await controller.Delete(999);

        Assert.IsType<NotFoundResult>(
            result);
    }





















}
