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
    public class ResiduosController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ResiduosController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var residuos = _context.Residuos.Include(r => r.Contenedor);
            return View(await residuos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var residuo = await _context.Residuos
                .Include(r => r.Contenedor)
                .FirstOrDefaultAsync(m => m.ResiduoId == id);

            if (residuo == null) return NotFound();

            return View(residuo);
        }

        public IActionResult Create()
        {
            ViewData["ContenedorId"] = new SelectList(_context.Contenedors, "ContenedorId", "TipoReciclaje");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResiduoId,Nombre,Descripcion,Imagen,Puntos,ContenedorId")] Residuo residuo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residuo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContenedorId"] = new SelectList(_context.Contenedors, "ContenedorId", "TipoReciclaje", residuo.ContenedorId);
            return View(residuo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var residuo = await _context.Residuos.FindAsync(id);
            if (residuo == null) return NotFound();

            ViewData["ContenedorId"] = new SelectList(_context.Contenedors, "ContenedorId", "TipoReciclaje", residuo.ContenedorId);
            return View(residuo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResiduoId,Nombre,Descripcion,Imagen,Puntos,ContenedorId")] Residuo residuo)
        {
            if (id != residuo.ResiduoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residuo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResiduoExists(residuo.ResiduoId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContenedorId"] = new SelectList(_context.Contenedors, "ContenedorId", "TipoReciclaje", residuo.ContenedorId);
            return View(residuo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var residuo = await _context.Residuos
                .Include(r => r.Contenedor)
                .FirstOrDefaultAsync(m => m.ResiduoId == id);

            if (residuo == null) return NotFound();

            return View(residuo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residuo = await _context.Residuos.FindAsync(id);
            if (residuo != null) _context.Residuos.Remove(residuo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResiduoExists(int id)
        {
            return _context.Residuos.Any(e => e.ResiduoId == id);
        }
    }
}