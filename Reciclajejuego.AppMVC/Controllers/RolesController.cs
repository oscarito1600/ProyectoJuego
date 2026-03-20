using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ReciclajeJuego.Controllers
{
    public class RolesController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public RolesController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // LISTAR ROLES
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        // MOSTRAR FORMULARIO CREAR
        public IActionResult Create()
        {
            return View();
        }

        // GUARDAR NUEVO ROL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // MOSTRAR FORMULARIO EDITAR
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return NotFound();

            return View(rol);
        }

        // GUARDAR CAMBIOS EDITAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol rol)
        {
            if (id != rol.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Roles.Any(e => e.Id == rol.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // CONFIRMAR ELIMINAR
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rol == null) return NotFound();

            return View(rol);
        }

        // ELIMINAR DEFINITIVO
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // DETALLES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rol == null) return NotFound();

            return View(rol);
        }
    }
}