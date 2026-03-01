using Microsoft.AspNetCore.Mvc;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaAjustesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return RedirectToAction("Index", "PantallaIniciarSesion");
        }

        public IActionResult CrearCuenta()
        {
            return RedirectToAction("Index", "PantallaCrearCuenta");
        }

        public IActionResult RegistroProgreso()
        {
            return RedirectToAction("Index", "PantallaRegistroProgreso");
        }

        public IActionResult Volver()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}