using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    // Cambiado a singular: ContenedorController
    public class ContenedorController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ContenedorController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: Contenedor
        public async Task<IActionResult> Index()
        {
            // Usar .Set<Contenedor>() resuelve el error de inferencia de tipo
            return View(await _context.Set<Contenedor>().ToListAsync());
        }

        // GET: Contenedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contenedor = await _context.Set<Contenedor>()
                .FirstOrDefaultAsync(m => m.ContenedorId == id);

            if (contenedor == null) return NotFound();

            return View(contenedor);
        }

        // GET: Contenedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contenedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContenedorId,TipoReciclaje,Color")] Contenedor contenedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contenedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contenedor);
        }

        // GET: Contenedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contenedor = await _context.Set<Contenedor>().FindAsync(id);
            if (contenedor == null) return NotFound();

            return View(contenedor);
        }

        // POST: Contenedor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContenedorId,TipoReciclaje,Color")] Contenedor contenedor)
        {
            if (id != contenedor.ContenedorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contenedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContenedorExists(contenedor.ContenedorId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contenedor);
        }

        // GET: Contenedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contenedor = await _context.Set<Contenedor>()
                .FirstOrDefaultAsync(m => m.ContenedorId == id);

            if (contenedor == null) return NotFound();

            return View(contenedor);
        }

        // POST: Contenedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contenedor = await _context.Set<Contenedor>().FindAsync(id);
            if (contenedor != null)
            {
                _context.Set<Contenedor>().Remove(contenedor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContenedorExists(int id)
        {
            // Forzamos el tipo <Contenedor> para eliminar el error CS1503/CS0411
            return _context.Set<Contenedor>().Any(e => e.ContenedorId == id);
        }
    }
}