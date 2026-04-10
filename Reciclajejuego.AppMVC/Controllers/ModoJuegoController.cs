using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class ModoJuegoController : Controller
    {
        private readonly ReciclajeJuegoContext _context = null!;

        public ModoJuegoController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // ============================
        // INDEX MEJORADO
        // ============================
        public async Task<IActionResult> Index(string buscar, string orden, int pagina = 1)
        {
            int registrosPorPagina = 5;

            var datos = _context.ModosJuego.AsQueryable();

            // 🔎 FILTRO
            if (!string.IsNullOrEmpty(buscar))
            {
                datos = datos.Where(m => m.Nombre.Contains(buscar));
            }

            // 🔽 ORDEN
            switch (orden)
            {
                case "nombre":
                    datos = datos.OrderBy(m => m.Nombre);
                    break;

                default:
                    datos = datos.OrderByDescending(m => m.Id); // recientes
                    break;
            }

            // 🔢 TOTAL
            var totalRegistros = await datos.CountAsync();

            // 📄 PAGINACIÓN
            var lista = await datos
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            // 📦 VIEWBAG
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            ViewBag.PaginaActual = pagina;
            ViewBag.Buscar = buscar;
            ViewBag.Orden = orden;

            return View(lista);
        }

        // ============================
        // CREATE
        // ============================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModoJuego modoJuego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modoJuego);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Modo creado correctamente ✅";
                return RedirectToAction(nameof(Index));
            }
            return View(modoJuego);
        }

        // ============================
        // EDIT
        // ============================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var modoJuego = await _context.ModosJuego.FindAsync(id);

            if (modoJuego == null)
                return NotFound();

            return View(modoJuego);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ModoJuego modoJuego)
        {
            if (id != modoJuego.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(modoJuego);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Modo actualizado ✏️";
                return RedirectToAction(nameof(Index));
            }

            return View(modoJuego);
        }

        // ============================
        // DELETE
        // ============================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var modoJuego = await _context.ModosJuego
                .FirstOrDefaultAsync(m => m.Id == id);

            if (modoJuego == null)
                return NotFound();

            return View(modoJuego);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modoJuego = await _context.ModosJuego.FindAsync(id);

            if (modoJuego != null)
            {
                _context.ModosJuego.Remove(modoJuego);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Modo eliminado 🗑️";
            }

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // DETAILS
        // ============================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var modoJuego = await _context.ModosJuego
                .FirstOrDefaultAsync(m => m.Id == id);

            if (modoJuego == null)
                return NotFound();

            return View(modoJuego);
        }
    }
}
