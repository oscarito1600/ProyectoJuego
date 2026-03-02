using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Residuo
    {
        public int ResiduoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(300)]
        public string Descripcion { get; set; } = null!;

        // Ruta relativa de la imagen guardada en wwwroot/imagenes
        public string? Imagen { get; set; }

        [Range(0, 1000, ErrorMessage = "Los puntos deben estar entre 0 y 1000")]
        public int Puntos { get; set; }

        [Required]
        public int ContenedorId { get; set; }

        public virtual Contenedor Contenedor { get; set; } = null!;
    }
}