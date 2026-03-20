using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class ModoJuegosController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ModoJuegosController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: ModoJuegos
        public async Task<IActionResult> Index()
        {
            var modos = await _context.ModoJuegos.ToListAsync();
            return View(modos);
        }

        // GET: ModoJuegos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var modoJuego = await _context.ModoJuegos
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);

            if (modoJuego == null) return NotFound();

            return View(modoJuego);
        }

        // GET: ModoJuegos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModoJuegos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModoJuegoId,Nombre")] ModoJuegos modoJuego)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(modoJuego);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "No se pudo guardar el modo de juego. Verifica los datos y que no exista duplicado.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Intenta nuevamente.");
                }
            }
            return View(modoJuego);
        }

        // GET: ModoJuegos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var modoJuego = await _context.ModoJuegos.FindAsync(id);
            if (modoJuego == null) return NotFound();

            return View(modoJuego);
        }

        // POST: ModoJuegos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModoJuegoId,Nombre")] ModoJuegos modoJuego)
        {
            if (id != modoJuego.ModoJuegoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modoJuego);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModoJuegoExists(modoJuego.ModoJuegoId))
                        return NotFound();
                    else
                        throw;
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "No se pudo actualizar el modo de juego. Verifica los datos y que no exista duplicado.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Intenta nuevamente.");
                }
            }
            return View(modoJuego);
        }

        // GET: ModoJuegos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var modoJuego = await _context.ModoJuegos
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);

            if (modoJuego == null) return NotFound();

            return View(modoJuego);
        }

        // POST: ModoJuegos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modoJuego = await _context.ModoJuegos.FindAsync(id);
            if (modoJuego != null)
            {
                // ⚠️ Si ModoJuego tiene relaciones, verifica antes de eliminar para no romper FK
                _context.ModoJuegos.Remove(modoJuego);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Verifica existencia para concurrencia
        private bool ModoJuegoExists(int id)
        {
            return _context.ModoJuegos.Any(e => e.ModoJuegoId == id);
        }
    }
}