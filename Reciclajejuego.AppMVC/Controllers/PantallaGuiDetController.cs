using Microsoft.AspNetCore.Mvc;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaGuiDetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}