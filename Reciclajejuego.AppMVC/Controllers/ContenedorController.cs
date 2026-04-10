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
    public class ContenedorController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public ContenedorController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: Contenedors
        // Añadimos los parámetros buscarNombre y buscarColor para recibir los datos del formulario
        public async Task<IActionResult> Index(string buscarNombre, string buscarColor)
        {
            // 1. Preparamos la consulta inicial sobre la base de datos
            var consulta = _context.Contenedores.AsQueryable();

            // 2. Aplicamos filtros si el usuario escribió algo en los inputs
            if (!string.IsNullOrEmpty(buscarNombre))
            {
                consulta = consulta.Where(c => c.TipoReciclaje.Contains(buscarNombre));
            }

            if (!string.IsNullOrEmpty(buscarColor))
            {
                consulta = consulta.Where(c => c.Color.Contains(buscarColor));
            }

            // 3. Aplicamos el orden y el límite 
            var contenedores = await consulta
                .OrderByDescending(c => c.Id)
                .Take(10)
                .ToListAsync();

            // 4. Mantenemos los valores en el ViewData para que el formulario no se borre al recargar
            ViewData["FiltroNombre"] = buscarNombre;
            ViewData["FiltroColor"] = buscarColor;

            return View(contenedores);
        }
        // GET: Contenedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenedor = await _context.Contenedores
                .FirstOrDefaultAsync(m => m.Id == id);
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

        // POST: Contenedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Cambiamos ContenedorId por Id en el Bind
        public async Task<IActionResult> Create([Bind("Id,TipoReciclaje,Color")] Contenedor contenedor)
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

            var contenedor = await _context.Contenedores.FindAsync(id);
            if (contenedor == null)
            {
                return NotFound();
            }
            return View(contenedor);
        }

        // POST: Contenedor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Cambiamos ContenedorId por Id en el Bind y en la validación
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoReciclaje,Color")] Contenedor contenedor)
        {

            if (id != contenedor.Id)
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
                    if (!ContenedorExists(contenedor.Id))
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

            var contenedor = await _context.Contenedores
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var contenedor = await _context.Contenedores.FindAsync(id);
            if (contenedor != null)
            {
                _context.Contenedores.Remove(contenedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContenedorExists(int id)
        {
            return _context.Contenedores.Any(e => e.Id == id);
        }
    }
}