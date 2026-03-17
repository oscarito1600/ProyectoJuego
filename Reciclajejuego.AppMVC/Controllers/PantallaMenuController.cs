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

        // BOTÓN AJUSTES
        public IActionResult Ajustes()
        {
            return RedirectToAction("Index", "PantallaAjustes");
        }

        public IActionResult EnProceso()
        {
            return View();
        }
    }
}