using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reciclajejuego.AppMVC.Models;

namespace Reciclajejuego.AppMVC.Controllers
{
    public class ResiduosController : Controller
    {
        private readonly ReciclajeJuegoContext _context;
        private readonly IWebHostEnvironment _env;

        public ResiduosController(ReciclajeJuegoContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            var residuos = _context.Residuos.Include(r => r.Contenedor);
            return View(await residuos.ToListAsync());
        }

        // ================= CREATE GET =================
        public IActionResult Create()
        {
            ViewData["ContenedorId"] =
                new SelectList(_context.Contenedors,
                "ContenedorId", "TipoReciclaje");

            return View();
        }

        // ================= CREATE POST =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Residuo residuo,
            IFormFile archivoImagen)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ContenedorId"] =
                    new SelectList(_context.Contenedors,
                    "ContenedorId", "TipoReciclaje",
                    residuo.ContenedorId);

                return View(residuo);
            }

            if (archivoImagen != null && archivoImagen.Length > 0)
            {
                string carpeta = Path.Combine(_env.WebRootPath, "imagenes");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string nombreUnico =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(archivoImagen.FileName);

                string ruta = Path.Combine(carpeta, nombreUnico);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await archivoImagen.CopyToAsync(stream);
                }

                residuo.Imagen = "/imagenes/" + nombreUnico;
            }

            _context.Add(residuo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ================= EDIT GET =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var residuo = await _context.Residuos.FindAsync(id);
            if (residuo == null) return NotFound();

            ViewData["ContenedorId"] =
                new SelectList(_context.Contenedors,
                "ContenedorId", "TipoReciclaje",
                residuo.ContenedorId);

            return View(residuo);
        }

        // ================= EDIT POST =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Residuo residuo,
            IFormFile archivoImagen)
        {
            if (id != residuo.ResiduoId)
                return NotFound();

            var residuoBD = await _context.Residuos
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ResiduoId == id);

            if (residuoBD == null)
                return NotFound();

            if (archivoImagen != null && archivoImagen.Length > 0)
            {
                string carpeta = Path.Combine(_env.WebRootPath, "imagenes");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string nombreUnico =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(archivoImagen.FileName);

                string ruta = Path.Combine(carpeta, nombreUnico);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await archivoImagen.CopyToAsync(stream);
                }

                residuo.Imagen = "/imagenes/" + nombreUnico;
            }
            else
            {
                residuo.Imagen = residuoBD.Imagen;
            }

            _context.Update(residuo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var residuo = await _context.Residuos
                .Include(r => r.Contenedor)
                .FirstOrDefaultAsync(m => m.ResiduoId == id);

            if (residuo == null) return NotFound();

            return View(residuo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residuo = await _context.Residuos.FindAsync(id);

            if (residuo != null)
            {
                if (!string.IsNullOrEmpty(residuo.Imagen))
                {
                    string rutaImagen = Path.Combine(
                        _env.WebRootPath,
                        residuo.Imagen.TrimStart('/'));

                    if (System.IO.File.Exists(rutaImagen))
                        System.IO.File.Delete(rutaImagen);
                }

                _context.Residuos.Remove(residuo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}