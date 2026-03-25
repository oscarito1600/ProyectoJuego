using Microsoft.AspNetCore.Mvc;
using Reciclajejuego.AppMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        // Añadimos 'Rol' al Bind por si acaso, aunque lo asignemos manualmente abajo
        public async Task<IActionResult> Index([Bind("Nombre,Correo,Contrasena")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Aseguramos que los valores coincidan con el tipo de dato (String)
                    usuario.MejorPuntaje = 0;
                    usuario.CuentaGoogle = "No vinculada"; // Debe ser un string, no un bool
                    usuario.Rol = "Jugador"; // Asignamos un rol por defecto para evitar nulos

                    _context.Add(usuario);
                    await _context.SaveChangesAsync();

                    // Redirige al menú principal
                    return RedirectToAction("Index", "PantallaMenu");
                }
                catch (DbUpdateException)
                {
                    // Si el correo ya existe en la base de datos
                    ModelState.AddModelError("Correo", "Este correo electrónico ya está registrado.");
                }
            }
            // Si el modelo no es válido, vuelve a la vista con los errores
            return View(usuario);
        }
    }
}