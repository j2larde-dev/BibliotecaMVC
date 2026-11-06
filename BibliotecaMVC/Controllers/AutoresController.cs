using AspNetCoreGeneratedDocument;
using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BibliotecaMVC.Controllers
{
    [Authorize]
    public class AutoresController : Controller
    {
        private readonly BibliotecaContext _context;

        private readonly ILogger<AutoresController> _logger;

        public AutoresController(BibliotecaContext context, ILogger<AutoresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Consultando el listado de Autores");

            try
            {
                var autores = await _context.Autores.ToListAsync();
                return View(autores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al consultar el listado de autores");
                throw;
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Consultando los detalles del autor con identificador {AutorId}", id);

            try
            {
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    _logger.LogWarning("No se encontro el autor solicitado con identificador {AutorId}", id);
                    return NotFound();
                }

                return View(autor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al consultar los detalles del autor con identificador {AutorId}", id);
                throw;
            }
        }

 
        [Authorize(Roles = "Admin, Bibliotecario")]
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando la operacion de creacion de un autor");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Create(Autor autor)
        {
            _logger.LogInformation("Iniciando la operacion de creacion de un autor");

            if (!ModelState.IsValid)
            {
                _logger.LogError("El autor enviado no cumple con los requerimientos minimos");
                return View(autor);
            }
            try
            {
                _context.Autores.Add(autor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al crear el autor");
                throw;
            }
        }

        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Iniciando la operacion de edicion del autor con identificador {AutorId}", id);

            try
            {
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound();
                }

                return View(autor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al consultar los detalles del autor para su edicion con identificador {AutorId}", id);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {
            _logger.LogInformation("Iniciando la edicion del autor con identificador {AutorId}", id);

            if (id != autor.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            try
            {
                var exists = await _context.Autores.AnyAsync(a => a.Id == id);

                if (!exists)
                {
                    _logger.LogWarning("No se encontro el autor solicitado con identificador {AutorId}", id);
                    return NotFound();
                }

                _context.Update(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error editar el autor con identificador {AutorId}", id);
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando la eliminacion del autor con identificador {AutorId}", id);

            try
            {
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    _logger.LogWarning("No se encontro el autor solicitado para eliminar con identificador {AutorId}", id);
                    return NotFound();
                }

                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error eliminar el autor con identificador {AutorId}", id);
                throw;
            }
        }
    }
}
