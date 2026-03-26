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
    public class ContenedorsController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ContenedorsController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: Contenedors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contenedors.ToListAsync());
        }

        // GET: Contenedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenedor = await _context.Contenedors
                .FirstOrDefaultAsync(m => m.ContenedorId == id);
            if (contenedor == null)
            {
                return NotFound();
            }

            return View(contenedor);
        }

        // GET: Contenedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contenedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Contenedors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenedor = await _context.Contenedors.FindAsync(id);
            if (contenedor == null)
            {
                return NotFound();
            }
            return View(contenedor);
        }

        // POST: Contenedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContenedorId,TipoReciclaje,Color")] Contenedor contenedor)
        {
            if (id != contenedor.ContenedorId)
            {
                return NotFound();
            }

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
            return View(contenedor);
        }

        // GET: Contenedors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenedor = await _context.Contenedors
                .FirstOrDefaultAsync(m => m.ContenedorId == id);
            if (contenedor == null)
            {
                return NotFound();
            }

            return View(contenedor);
        }

        // POST: Contenedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contenedor = await _context.Contenedors.FindAsync(id);
            if (contenedor != null)
            {
                _context.Contenedors.Remove(contenedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContenedorExists(int id)
        {
            return _context.Contenedors.Any(e => e.ContenedorId == id);
        }
    }
}
