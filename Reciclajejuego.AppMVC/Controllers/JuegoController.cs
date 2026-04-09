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
    public class JuegoController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public JuegoController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: Juegoes
        public async Task<IActionResult> Index(string buscarUsuario, string buscarEstado)
        {
            // 1. Iniciamos la consulta incluyendo la relación con Usuario (si existe en tu modelo)
            var consulta = _context.Juegos.Include(p => p.Usuarios).AsQueryable();

            // 2. Filtro por Usuario
            if (!string.IsNullOrEmpty(buscarUsuario))
            {
                // Ajusta 'Nombre' según cómo se llame la propiedad en tu clase Usuario
                consulta = consulta.Where(p => p.Usuarios.Nombre.Contains(buscarUsuario));
            }

            // 3. Filtro por Estado (Completada, En curso, etc.)
            if (!string.IsNullOrEmpty(buscarEstado))
            {
                consulta = consulta.Where(p => p.Estado == buscarEstado);
            }

            // 4. Ordenar por fecha más reciente
            var partidas = await consulta
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();

            // 5. Guardar valores para la vista
            ViewData["FiltroUsuario"] = buscarUsuario;
            ViewData["FiltroEstado"] = buscarEstado;

            return View(partidas);
        }

        // GET: Juegoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos
                .Include(j => j.ModoJuego)
                .Include(j => j.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        // GET: Juegoes/Create
        public IActionResult Create()
        {
            // AJUSTE: Se cambia el tercer parámetro para mostrar el Nombre en el ComboBox
            ViewData["ModoJuegoId"] = new SelectList(_context.ModosJuego, "ModoJuegoId", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");
            return View();
        }

        // POST: Juegoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JuegoId,UsuarioId,ModoJuegoId,PuntuacionActual,FechaInicio,Estado")] Juego juego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(juego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // AJUSTE: Se repite el cambio por si el formulario tiene errores y se recarga la página
            ViewData["ModoJuegoId"] = new SelectList(_context.ModosJuego, "ModoJuegoId", "Nombre", juego.ModoJuegoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", juego.UsuarioId);
            return View(juego);
        }

        // GET: Juegoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos.FindAsync(id);
            if (juego == null)
            {
                return NotFound();
            }
            // AJUSTE: Cambiamos IDs por Nombres para que el editor sea legible
            ViewData["ModoJuegoId"] = new SelectList(_context.ModosJuego, "ModoJuegoId", "Nombre", juego.ModoJuegoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", juego.UsuarioId);
            return View(juego);
        }

        // POST: Juegoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JuegoId,UsuarioId,ModoJuegoId,PuntuacionActual,FechaInicio,Estado")] Juego juego)
        {
            if (id != juego.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(juego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JuegoExists(juego.Id))
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
            // AJUSTE: Se repite el cambio en el POST de Edit
            ViewData["ModoJuegoId"] = new SelectList(_context.ModosJuego, "ModoJuegoId", "Nombre", juego.ModoJuegoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre", juego.UsuarioId);
            return View(juego);
        }

        // GET: Juegoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos
                .Include(j => j.ModoJuego)
                .Include(j => j.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        // POST: Juegoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var juego = await _context.Juegos.FindAsync(id);
            if (juego != null)
            {
                _context.Juegos.Remove(juego);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JuegoExists(int id)
        {
            return _context.Juegos.Any(e => e.Id == id);
        }
    }
}