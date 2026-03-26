using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class DetallesModoController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public DetallesModoController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reciclajeJuegoContext = _context.DetallesModos.Include(d => d.ModoJuego);
            return View(await reciclajeJuegoContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var detallesModo = await _context.DetallesModos
                .Include(d => d.ModoJuego)
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);

            if (detallesModo == null) return NotFound();

            return View(detallesModo);
        }

        public IActionResult Create()
        {
            // AJUSTE: Se cambia "ModoJuegoId" por "Nombre" para mostrar el texto
            ViewData["ModoJuegoId"] = new SelectList(_context.ModoJuegos, "ModoJuegoId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModoJuegoId,Vidas,TiempoLimite,ComboMaximo")] DetallesModo detallesModo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallesModo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModoJuegoId"] = new SelectList(_context.ModoJuegos, "ModoJuegoId", "Nombre", detallesModo.ModoJuegoId);
            return View(detallesModo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var detallesModo = await _context.DetallesModos.FindAsync(id);
            if (detallesModo == null) return NotFound();

            ViewData["ModoJuegoId"] = new SelectList(_context.ModoJuegos, "ModoJuegoId", "Nombre", detallesModo.ModoJuegoId);
            return View(detallesModo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModoJuegoId,Vidas,TiempoLimite,ComboMaximo")] DetallesModo detallesModo)
        {
            if (id != detallesModo.ModoJuegoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallesModo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesModoExists(detallesModo.ModoJuegoId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModoJuegoId"] = new SelectList(_context.ModoJuegos, "ModoJuegoId", "Nombre", detallesModo.ModoJuegoId);
            return View(detallesModo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var detallesModo = await _context.DetallesModos
                .Include(d => d.ModoJuego)
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);

            if (detallesModo == null) return NotFound();

            return View(detallesModo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallesModo = await _context.DetallesModos.FindAsync(id);
            if (detallesModo != null) _context.DetallesModos.Remove(detallesModo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesModoExists(int id)
        {
            return _context.DetallesModos.Any(e => e.ModoJuegoId == id);
        }
    }
}
