using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reciclajejuego.AppMVC.Models;
using System.Linq;
using System.IO;
using System;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class PantallaGuiaController : Controller
    {
        private readonly ReciclajeJuegoContext _context;
        private readonly IWebHostEnvironment _env;

        public PantallaGuiaController(ReciclajeJuegoContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.Contenedores = new SelectList(
                _context.Contenedores.ToList(),
                "ContenedorId",
                "TipoReciclaje"
            );

            return View();
        }

        // =========================
        // CREAR CONTENEDOR
        // =========================
        [HttpPost]
        public IActionResult CrearContenedor(string tipoReciclaje, string color)
        {
            if (string.IsNullOrWhiteSpace(tipoReciclaje))
            {
                return RedirectToAction("Index");
            }

            var contenedor = new Contenedor
            {
                TipoReciclaje = tipoReciclaje.Trim(),
                Color = color ?? "#000000"
            };

            _context.Contenedores.Add(contenedor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // CREAR RESIDUO
        // =========================
        [HttpPost]
        public IActionResult CrearResiduo(
            string nombre,
            string descripcion,
            IFormFile imagenArchivo,
            int contenedorId
        )
        {
            string nombreImagen = "";

            if (imagenArchivo != null)
            {
                string carpeta = Path.Combine(_env.WebRootPath, "imagenes");

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(imagenArchivo.FileName);

                string rutaCompleta = Path.Combine(carpeta, nombreImagen);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    imagenArchivo.CopyTo(stream);
                }
            }

            var residuo = new Residuo
            {
                Nombre = nombre,
                Descripcion = descripcion,
                ImagenUrl = "/imagenes/" + nombreImagen,
                ContenedorId = contenedorId
            };

            _context.Residuos.Add(residuo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
