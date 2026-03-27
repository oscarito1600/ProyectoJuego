using Microsoft.AspNetCore.Mvc;
using Reciclajejuego.AppMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaCrearCuentaController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public PantallaCrearCuentaController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: /PantallaCrearCuenta/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /PantallaCrearCuenta/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Nombre,Correo,Contrasena")] Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            // 🔍 Verificar si el correo ya existe
            var correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo == usuario.Correo);

            if (correoExiste)
            {
                ModelState.AddModelError("Correo", "Este correo ya está registrado.");
                return View(usuario);
            }

            try
            {
                // ✅ CORREGIDO
                usuario.MejoresPuntajes = new List<MejorPuntaje>();
                usuario.EsCuentaGoogle = false; // porque en tu modelo es string

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "PantallaMenu");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Error al guardar los datos.");
                return View(usuario);
            }
        }
    }
}