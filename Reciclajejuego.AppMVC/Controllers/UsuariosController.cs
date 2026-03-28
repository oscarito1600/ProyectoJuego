using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;
using System.Data;


namespace Reciclajejuego.AppMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public UsuariosController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            // Agregamos .Include(u => u.Rol) para que en la tabla se vea el nombre del rol y no solo el ID
            return View(await _context.Usuarios.Include(u => u.Rol).ToListAsync());
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {
            // Usamos el nombre de la clase con su carpeta para evitar confusiones
            // Cambia 'Roles' por 'Roles' si así se llama exactamente en tu archivo roles.cs
            var listaRoles = await _context.Rol.ToListAsync<Reciclajejuego.AppMVC.Models.Rol>();

            if (listaRoles != null && listaRoles.Any())
            {
                // Revisa si en tu base de datos las columnas son Id y Nombre. 
                // Si no, cámbialas aquí abajo.
                ViewData["IdRol"] = new SelectList(listaRoles, "Id", "Nombre");
            }
            else
            {
                ViewData["IdRol"] = new SelectList(new List<Reciclajejuego.AppMVC.Models.Rol>(), "Id", "Nombre");
            }

            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                // Inicializar lista de puntajes para evitar errores de nulos
                usuario.MejoresPuntajes = new List<MejorPuntaje>();

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Si el modelo NO es válido (ej: falta un campo), volvemos a cargar los roles 
            // para que el desplegable no aparezca vacío al recargar la página.
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.Id);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            // Cargamos los roles también al editar para poder cambiarlos
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.Id);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
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
                    if (!UsuarioExists(usuario.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Recargamos roles si el formulario tiene errores
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.Id);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.Rol) // Incluimos el rol para saber qué estamos borrando
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
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
