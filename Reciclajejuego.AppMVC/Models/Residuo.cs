using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    [Table("Residuos")]
    public partial class Residuo
    {
        public int Id { get; set; } // ✅ PK corregida

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Descripcion { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Display(Name = "Imagen")]
        public string? ImagenUrl { get; set; } // ✅ corregido

        public int Puntos { get; set; } = 10;

        [Required(ErrorMessage = "Debe seleccionar un contenedor")]
        public int ContenedorId { get; set; }

        public virtual Contenedor Contenedor { get; set; } = null!;
    }
}