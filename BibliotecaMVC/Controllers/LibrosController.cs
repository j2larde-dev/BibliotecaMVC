using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BibliotecaMVC.Controllers
{
    [Authorize] //Todo el módulo de Libros requiere sesión iniciada
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;
        private readonly ILogger<LibrosController> _logger;

        public LibrosController(BibliotecaContext context, ILogger<LibrosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Consultando el listado de Libros");
            var libros = await _context.Libros.ToListAsync();

            return View(libros);
        }

        [Authorize]     //Requiere autenticación
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Consultando los detalles del libro con identificador {LibroId}", id);
            try
            {
                var libro = await _context.Libros.FindAsync(id);

                if (libro == null)
                {
                    _logger.LogWarning("No se encontró el libro solicitado con identificador {LibroId}", id);
                    return NotFound();
                }
                return View(libro);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al consultar los detalles del libro con identificador {LibroId}", id);
                throw;
            }
        }



        [Authorize(Roles = "Admin,Bibliotecario")]
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando la operación de creación de un libro");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Bibliotecario")]
        public async Task<IActionResult> Create(Libro libro)
        {
            _logger.LogInformation("Iniciando la operación de creación de un libro");

            if (!ModelState.IsValid)
            {
                _logger.LogError("El libro enviado no cumple con los requisitos mínimos");
                return View(libro);
            }

            try
            {
                _context.Libros.Add(libro);

                await _context.SaveChangesAsync();

                return RedirectToAction(
                    nameof(Index),
                    new { success = "Libro creado correctamente" }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al crear el libro");

                throw;
            }
        }


        [Authorize(Roles = "Admin,Bibliotecario")]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation(
                "Iniciando la operación de edición del libro con identificador {LibroId}",
                id
            );

            try
            {
                var libro = await _context.Libros.FindAsync(id);

                if (libro == null)
                {
                    _logger.LogWarning(
                        "No se encontró el libro solicitado con identificador {LibroId}",
                        id
                    );

                    return NotFound();
                }

                return View(libro);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo un error al consultar los detalles del libro para su edición con identificador {LibroId}",
                    id
                );

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Bibliotecario")]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            _logger.LogInformation("Iniciando la edición del libro con identificador {LibroId}", id);

            if (id != libro.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("El libro enviado no cumple con los requisitos mínimos para su actualización");

                return View(libro);
            }
            try
            {
                var exists = await _context.Libros.AnyAsync(l => l.ID == id);

                if (!exists)
                {
                    _logger.LogWarning("No se encontró el libro solicitado con identificador {LibroId}", id);

                    return NotFound();
                }
                _context.Update(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { success = "Cambios guardados correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al editar el libro con identificador {LibroId}", id);
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Bibliotecario")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation( "Iniciando la eliminación del libro con identificador {LibroId}", id );

            try
            {
                var libro = await _context.Libros.FindAsync(id);

                if (libro == null)
                {
                    _logger.LogWarning(  "No se encontró el libro solicitado para eliminar con identificador {LibroId}", id );
                    return NotFound();
                }

                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();

                return RedirectToAction( nameof(Index), new { success = "Libro eliminado correctamente" } );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al eliminar el libro con identificador {LibroId}", id );
                throw;
            }
        }
    }
}
