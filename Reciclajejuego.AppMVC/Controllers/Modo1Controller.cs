using Microsoft.AspNetCore.Mvc;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class Modo1Controller : Controller
    {
        private static int puntos = 0;

        // Cargar la vista del juego
        public IActionResult Index()
        {
            ViewBag.Puntos = puntos;
            return View();
        }

        // Reiniciar juego
        public IActionResult Reiniciar()
        {
            puntos = 0;
            return RedirectToAction("Index");
        }

        // Guardar puntos (opcional)
        [HttpPost]
        public IActionResult GuardarPuntos(int nuevosPuntos)
        {
            puntos = nuevosPuntos;
            return Ok();
        }
    }
}