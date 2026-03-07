using Microsoft.AspNetCore.Mvc;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaMenuController : Controller
    {
        // Pantalla principal del menú
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Clasico()
        {
            return View("EnProceso");
        }

        public IActionResult ContraTiempo()
        {
            return View("EnProceso");
        }

        public IActionResult Aprendizaje()
        {
            return View("EnProceso");
        }

        public IActionResult Guia()
        {
            return View("EnProceso");
        }

        public IActionResult EnProceso()
        {
            return View();
        }
    }
}