using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        public IActionResult Index()
        {
            List<Autor> Autores = new List<Autor>()
            {
                new Autor
                {
                    ID = 1,
                    Nombre = "Gabriel",
                    Apellido = "García Márquez",
                    Nacionalidad = "Colombiana",
                    FechaNacimiento = new DateTime(1927, 3, 6),
                    Activo = false
                },

                new Autor
                {
                    ID = 2,
                    Nombre = "Isabel",
                    Apellido = "Allende",
                    Nacionalidad = "Chilena",
                    FechaNacimiento = new DateTime(1942, 8, 2),
                    Activo = true
                },

                new Autor
                {
                    ID = 3,
                    Nombre = "Mario",
                    Apellido = "Vargas Llosa",
                    Nacionalidad = "Peruana",
                    FechaNacimiento = new DateTime(1936, 3, 28),
                    Activo = true
                },

                new Autor
                {
                    ID = 4,
                    Nombre = "Jorge Luis",
                    Apellido = "Borges",
                    Nacionalidad = "Argentina",
                    FechaNacimiento = new DateTime(1899, 8, 24),
                    Activo = false
                },

                new Autor
                {
                    ID = 5,
                    Nombre = "Mark",
                    Apellido = "Batterson",
                    Nacionalidad = "Estadounidense",
                    FechaNacimiento = new DateTime(1969, 11, 5),
                    Activo = true
                },

                new Autor
                {
                    ID = 6,
                    Nombre = "Laura",
                    Apellido = "Esquivel",
                    Nacionalidad = "Mexicana",
                    FechaNacimiento = new DateTime(1950, 9, 30),
                    Activo = true
                },
            };

            ViewBag.Nombre = "Juan Lara";
            ViewBag.Autores = Autores;

            return View();
        }
    }
}
