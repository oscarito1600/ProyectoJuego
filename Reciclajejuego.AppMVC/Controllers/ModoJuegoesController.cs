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
    public class ModoJuegoesController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ModoJuegoesController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: ModoJuegoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModoJuegos.ToListAsync());
        }

        // GET: ModoJuegoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modoJuego = await _context.ModoJuegos
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);
            if (modoJuego == null)
            {
                return NotFound();
            }

            return View(modoJuego);
        }

        // GET: ModoJuegoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModoJuegoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModoJuegoId,Nombre")] ModoJuego modoJuego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modoJuego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modoJuego);
        }

        // GET: ModoJuegoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modoJuego = await _context.ModoJuegos.FindAsync(id);
            if (modoJuego == null)
            {
                return NotFound();
            }
            return View(modoJuego);
        }

        // POST: ModoJuegoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModoJuegoId,Nombre")] ModoJuego modoJuego)
        {
            if (id != modoJuego.ModoJuegoId)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(modoJuego);
        }

        // GET: ModoJuegoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modoJuego = await _context.ModoJuegos
                .FirstOrDefaultAsync(m => m.ModoJuegoId == id);
            if (modoJuego == null)
            {
                return NotFound();
            }

            return View(modoJuego);
        }

        // POST: ModoJuegoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modoJuego = await _context.ModoJuegos.FindAsync(id);
            if (modoJuego != null)
            {
                _context.ModoJuegos.Remove(modoJuego);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModoJuegoExists(int id)
        {
            return _context.ModoJuegos.Any(e => e.ModoJuegoId == id);
        }
    }
}
