using Microsoft.AspNetCore.Mvc;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaMenuController : Controller
    {
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

        public IActionResult Ajustes() // ← BOTÓN TUERCA
        {
            return View("Ajustes");
        }

        public IActionResult EnProceso()
        {
            return View();
        }
    }
}