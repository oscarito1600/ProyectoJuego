using Microsoft.AspNetCore.Mvc;
using Reciclajejuego.AppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaInisiarSesionController : Controller
    {
        private readonly ReciclajeJuegoContext _context;

        public PantallaInisiarSesionController(ReciclajeJuegoContext context)
        {
            _context = context;
        }

        // GET: /PantallaInisiarSesion/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /PantallaInisiarSesion/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string correo, string contrasena)
        {
            // Buscamos en la tabla de usuarios de tu base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contrasena == contrasena);

            if (usuario != null)
            {
                // Si lo encuentra, enviamos los datos a la vista de éxito
                ViewBag.Nombre = usuario.Nombre;
                ViewBag.Puntaje = usuario.MejorPuntaje;
                return View("Exito");
            }
            else
            {
                // Si no existe, enviamos un mensaje de error a la misma vista index
                ViewBag.Error = "Cuenta no encontrada. Verifica tu correo y contraseña.";
                return View();
            }
        }
    }
}