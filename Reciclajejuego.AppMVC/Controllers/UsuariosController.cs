using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public UsuariosController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // ✅ GET: Usuarios (CON FILTRO + TOP)
        public async Task<IActionResult> Index(string buscar, int top = 10)
        {
            var query = _context.Usuarios
                .Include(u => u.Rol)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(u =>
                    u.Nombre.Contains(buscar) ||
                    u.Correo.Contains(buscar));
            }

            var usuarios = await query
                .Take(top)
                .ToListAsync();

            ViewBag.Buscar = buscar;
            ViewBag.Top = top;

            return View(usuarios);
        }

        // ✅ GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // ✅ GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {
            var listaRoles = await _context.Rol.ToListAsync();
            ViewData["IdRol"] = new SelectList(listaRoles, "Id", "Nombre");
            return View();
        }

        // ✅ POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.MejoresPuntajes = new List<MejorPuntaje>();

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // ✅ GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Nombre", usuario.RolId);

            return View(usuario);
        }

        // ✅ POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuarios usuario)
        {
            if (id != usuario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // ✅ GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // ✅ POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}