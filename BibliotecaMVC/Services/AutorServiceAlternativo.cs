using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    public class AutorServiceAlternativo : IAutorService
    {
        private readonly List<Autor> _autores;

        public AutorServiceAlternativo()
        {
            _autores = new List<Autor>()
            {
                new Autor
                {
                    Id = 101,
                    Nombre = "Isabel Allende",
                    Nacionalidad = "Chilena",
                    FechaNacimiento = new DateTime(1942, 8, 2)
                },
                new Autor
                {
                    Id = 102,
                    Nombre = "Jorge Luis Borges",
                    Nacionalidad = "Argentina",
                    FechaNacimiento = new DateTime(1899, 8, 24)
                },
                new Autor
                {
                    Id = 103,
                    Nombre = "Octavio Paz",
                    Nacionalidad = "Mexicana",
                    FechaNacimiento = new DateTime(1914, 3, 31)
                }
            };
        }

        public IEnumerable<Autor> ObtenerTodos()
        {
            return _autores;
        }

        public Autor? ObtenerPorId(int id)
        {
            return _autores.FirstOrDefault(a => a.Id == id);
        }
    }
}
