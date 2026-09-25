using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros.ToListAsync();
            return View(libros);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            _context.Add(libro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { success = "Libro creado correctamente" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            var exists = await _context.Libros.AnyAsync(l => l.ID == id);

            if (!exists)
            {
                return NotFound();
            }

            _context.Update(libro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { success = "Cambios guardados correctamente" });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
 
            return RedirectToAction(nameof(Index), new { success = "Libro eliminado correctamente" });
        }

    }
}
