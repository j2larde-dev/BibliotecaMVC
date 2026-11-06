using System.ComponentModel.DataAnnotations;

using BibliotecaMVC.Models;

namespace BibliotecaMVC.Tests;

public class LibroValidationTests
{
    [Fact]
    public void TituloEsObligatorio()
    {
        var libro = new Libro
        {
            Titulo = string.Empty
        };

        var errores = Validate(libro);

        Assert.Contains(
            errores,
            error => error.MemberNames.Contains(
                nameof(Libro.Titulo)));
    }

    [Fact]
    public void TituloNoPuedeSuperarLos150Caracteres()
    {
        var libro = new Libro
        {
            Titulo = new string('A', 151),
            Autor = "Autor"
        };

        var errores = Validate(libro);

        Assert.Contains(
            errores,
            error => error.MemberNames.Contains(
                nameof(Libro.Titulo)));
    }

    [Fact]
    public void TituloCon150CaracteresEsValido()
    {
        var libro = new Libro
        {
            Titulo = new string('A', 150),
            Autor = "Autor"
        };

        var errores = Validate(libro);

        Assert.DoesNotContain(
            errores,
            error => error.MemberNames.Contains(
                nameof(Libro.Titulo)));
    }

    [Fact]
    public void AutorEsObligatorio()
    {
        var libro = new Libro
        {
            Titulo = "Libro válido",
            Autor = string.Empty
        };

        var errores = Validate(libro);

        Assert.Contains(
            errores,
            error => error.MemberNames.Contains(
                nameof(Libro.Autor)));
    }

    [Fact]
    public void CategoriaNoPuedeSuperarLos50Caracteres()
    {
        var libro = new Libro
        {
            Titulo = "Libro válido",
            Autor = "Autor válido",
            Categoria = new string('B', 51)
        };

        var errores = Validate(libro);

        Assert.Contains(
            errores,
            error => error.MemberNames.Contains(
                nameof(Libro.Categoria)));
    }

    private static IReadOnlyCollection<ValidationResult>
        Validate(Libro libro)
    {
        var context =
            new ValidationContext(libro);

        var errores =
            new List<ValidationResult>();

        Validator.TryValidateObject(
            libro,
            context,
            errores,
            validateAllProperties: true);

        return errores;
    }
}
