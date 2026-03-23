using Microsoft.AspNetCore.Mvc;
using Reciclajejuego.AppMVC.Models;
using Microsoft.EntityFrameworkCore;

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
        // Al llamarse Index, esta será la página que cargue por defecto al entrar a la ruta
        public IActionResult Index()
        {
            return View();
        }

        // POST: /PantallaCrearCuenta/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Nombre,Correo,Contrasena")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Valores iniciales obligatorios según tu base de datos
                    usuario.MejoresPuntajes = 0;
                    usuario.EsCuentaGoogle = false;

                    _context.Add(usuario);
                    await _context.SaveChangesAsync();

                    // Redirige al menú principal tras crear la cuenta exitosamente
                    return RedirectToAction("Index", "PantallaMenu");
                }
                catch (DbUpdateException)
                {
                    // Manejo del índice único de correo en tu base de datos
                    ModelState.AddModelError("Correo", "Este correo electrónico ya está registrado.");
                }
            }
            // Si hay errores, regresamos a la vista Index.cshtml con los datos ingresados
            return View(usuario);
        }
    }
}