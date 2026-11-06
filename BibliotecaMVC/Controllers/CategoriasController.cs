using BibliotecaMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BibliotecaMVC.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly BibliotecaContext _context;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(
            BibliotecaContext context,
            ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            _logger.LogInformation(
                "Consultando el listado de categorías"
            );

            return View();
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            _logger.LogInformation(
                "Consultando los detalles de la categoría con identificador {CategoriaId}",
                id
            );

            try
            {
                var categoria = (object)null;

                if (categoria == null)
                {
                    _logger.LogWarning(
                        "No se encontró la categoría solicitada con identificador {CategoriaId}",
                        id
                    );

                    return NotFound();
                }

                return View(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo un error al consultar la categoría con identificador {CategoriaId}",
                    id
                );

                throw;
            }
        }

        [Authorize(Roles = "Admin,Bibliotecario")]
        public IActionResult Create()
        {
            _logger.LogInformation(
                "Iniciando la operación de creación de una categoría"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Bibliotecario")]
        public IActionResult Create(object categoria)
        {
            _logger.LogInformation(
                "Iniciando la operación de creación de una categoría"
            );

            if (!ModelState.IsValid)
            {
                _logger.LogError(
                    "La categoría enviada no cumple con los requisitos mínimos"
                );

                return View(categoria);
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo un error al crear la categoría"
                );

                throw;
            }
        }

        [Authorize(Roles = "Admin,Bibliotecario")]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation(
                "Iniciando la operación de edición de la categoría con identificador {CategoriaId}",
                id
            );

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo un error al consultar la categoría para edición con identificador {CategoriaId}",
                    id
                );

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Bibliotecario")]
        public IActionResult Edit(int id, object categoria)
        {
            _logger.LogInformation(
                "Iniciando la edición de la categoría con identificador {CategoriaId}",
                id
            );

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo un error al editar la categoría con identificador {CategoriaId}",
                    id
                );

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Bibliotecario")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(
                "Iniciando la eliminación de la categoría con identificador {CategoriaId}",
                id
            );

            try
            {
                _logger.LogWarning(
                    "No se encontró la categoría solicitada para eliminar con identificador {CategoriaId}",
                    id
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo un error al eliminar la categoría con identificador {CategoriaId}",
                    id
                );

                throw;
            }
        }
    }
}