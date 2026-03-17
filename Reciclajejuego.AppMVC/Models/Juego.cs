using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Juego
    {
        public int JuegoId { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El modo de juego es obligatorio")]
        public int ModoJuegoId { get; set; }

        [Display(Name = "Puntuación Final")]
        public int PuntuacionFinal { get; set; } = 0;

        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [RegularExpression("Finalizado|Pausado|Activo",
            ErrorMessage = "El estado debe ser: Finalizado, Pausado o Activo")]
        public string Estado { get; set; } = "Activo";

        public virtual ModoJuego ModoJuego { get; set; } = null!;

        public virtual Usuario Usuario { get; set; } = null!;
    }
}