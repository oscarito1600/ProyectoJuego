using Microsoft.AspNetCore.Mvc;

namespace EcoRecicla.Controllers
{
    public class PantallaAjustesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        public IActionResult CrearCuenta()
        {
            return View();
        }

        public IActionResult RegistroProgreso()
        {
            return View();
        }

        public IActionResult Volver()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}