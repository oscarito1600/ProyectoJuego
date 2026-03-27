using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    [Table("Juegos")]
    public partial class Juego
    {
        public int Id { get; set; } // ✅ PK corregida

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El modo de juego es obligatorio")]
        public int ModoJuegoId { get; set; }

        [Display(Name = "Puntuación Final")]
        public int PuntuacionFinal { get; set; } = 0; // ✅ corregido

        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [RegularExpression("Finalizado|Pausado|Activo",
            ErrorMessage = "El estado debe ser: Finalizado, Pausado o Activo")]
        public string Estado { get; set; } = "Activo";

        public virtual ModoJuego ModoJuego { get; set; } = null!;
        public virtual Usuarios Usuarios { get; set; } = null!;
    }
}