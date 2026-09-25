using BibliotecaMVC.Models;

namespace BibliotecaMVC.Repositories
{
    public class RepositorioEnMemoria : IRepositorioLibro
    {
        public IEnumerable<Libro> ObtenerTodos()
        {
            return new List<Libro>()
            {
                new Libro
                {
                    ID = 1,
                    Titulo = "Clean Code",
                    Autor = "Robert Martin",
                    Categoria = "Programación",
                    Precio = 35.5M,
                    Disponible = true
                },
                new Libro {
                    ID = 2,
                    Titulo = "Cien años de soledad",
                    Autor = "Gabriel García Márquez",
                    Categoria = "Literatura",
                    Precio = 18,
                    Disponible = false
                },
                new Libro {
                    ID = 3,
                    Titulo = "Biblia de Estudio ",
                    Autor = "Sociedades Biblicas",
                    Categoria = "Espiritualidad",
                    Precio = 20,
                    Disponible = true
                }
            };
        }
    }
}
