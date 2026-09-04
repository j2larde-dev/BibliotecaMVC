using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Services;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorService _servicio;

        public AutoresController(IAutorService servicio)
        {
            _servicio = servicio;
        }

        public IActionResult Index()
        {
            var autores = _servicio.ObtenerTodos();
            return View(autores);
        }

        public IActionResult Details(int id)
        {
            var autor = _servicio.ObtenerPorId(id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }
    }
}
