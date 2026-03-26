using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class ModoJuegoController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ModoJuegoController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: ModoJuego
        public async Task<IActionResult> Index()
        {
            var lista = await _context.ModoJuegos.ToListAsync();
            return View(lista);
        }

        // GET: ModoJuego/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var modoJuego = await _context.ModoJuegos
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);

            if (modoJuego == null)
                return NotFound();

            return View(modoJuego);
        }

        // GET: ModoJuego/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModoJuego/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModoJuego modoJuego)
        {
            if (ModelState.IsValid)
            {
                _context.ModoJuegos.Add(modoJuego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modoJuego);
        }

        // GET: ModoJuego/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var modoJuego = await _context.ModoJuegos.FindAsync(id);

            if (modoJuego == null)
                return NotFound();

            return View(modoJuego);
        }

        // POST: ModoJuego/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ModoJuego modoJuego)
        {
            if (id != modoJuego.ModoJuegoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modoJuego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModoJuegoExists(modoJuego.ModoJuegoId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(modoJuego);
        }

        // GET: ModoJuego/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var modoJuego = await _context.ModoJuegos
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);

            if (modoJuego == null)
                return NotFound();

            return View(modoJuego);
        }

        // POST: ModoJuego/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modoJuego = await _context.ModoJuegos.FindAsync(id);

            if (modoJuego != null)
                _context.ModoJuegos.Remove(modoJuego);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModoJuegoExists(int id)
        {
            return _context.ModoJuegos.Any(e => e.ModoJuegoId == id);
        }
    }
}
