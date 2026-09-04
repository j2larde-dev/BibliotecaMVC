using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    public class AutorService : IAutorService
    {
        private readonly List<Autor> _autores;

        public AutorService()
        {
            _autores = new List<Autor>()
            {
                new Autor
                {
                    Id = 1,
                    Nombre = "Gabriel García Márquez",
                    Nacionalidad = "Colombiana",
                    FechaNacimiento = new DateTime(1927, 3, 6)
                },
                new Autor
                {
                    Id = 2,
                    Nombre = "Mario Vargas Llosa",
                    Nacionalidad = "Peruana",
                    FechaNacimiento = new DateTime(1936, 3, 28)
                },
                new Autor
                {
                    Id = 3,
                    Nombre = "Isabel Allende",
                    Nacionalidad = "Chilena",
                    FechaNacimiento = new DateTime(1942, 8, 2)
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
