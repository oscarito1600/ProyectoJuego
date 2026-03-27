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
    public class AjustesController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public AjustesController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reciclajeJuegoContext = _context.Ajustes.Include(a => a.Usuario);
            return View(await reciclajeJuegoContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ajuste = await _context.Ajustes
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ajuste == null) return NotFound();

            return View(ajuste);
        }

        public IActionResult Create()
        {
            // AJUSTE: Cambiamos "UsuarioId" por "Nombre"
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AjustesId,UsuarioId,VolumenGeneral,VolumenMusica,VolumenEfectos")] Ajuste ajuste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ajuste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // AJUSTE: Cambiamos "UsuarioId" por "Nombre"
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", ajuste.UsuarioId);
            return View(ajuste);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ajuste = await _context.Ajustes.FindAsync(id);
            if (ajuste == null) return NotFound();

            // AJUSTE: Cambiamos "UsuarioId" por "Nombre"
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", ajuste.UsuarioId);
            return View(ajuste);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AjustesId,UsuarioId,VolumenGeneral,VolumenMusica,VolumenEfectos")] Ajuste ajuste)
        {
            if (id != ajuste.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ajuste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AjusteExists(ajuste.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            // AJUSTE: Cambiamos "UsuarioId" por "Nombre"
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", ajuste.UsuarioId);
            return View(ajuste);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ajuste = await _context.Ajustes
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ajuste == null) return NotFound();

            return View(ajuste);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ajuste = await _context.Ajustes.FindAsync(id);
            if (ajuste != null) _context.Ajustes.Remove(ajuste);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AjusteExists(int id)
        {
            return _context.Ajustes.Any(e => e.Id == id);
        }
    }
}