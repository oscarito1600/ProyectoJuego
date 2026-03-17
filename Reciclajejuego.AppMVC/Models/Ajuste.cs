using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Ajuste
    {
        public int AjustesId { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El volumen general es obligatorio")]
        [Range(0, 100, ErrorMessage = "Debe estar entre 0 y 100")]
        public int VolumenGeneral { get; set; }

        [Required(ErrorMessage = "El volumen de música es obligatorio")]
        [Range(0, 100, ErrorMessage = "Debe estar entre 0 y 100")]
        public int VolumenMusica { get; set; }

        [Required(ErrorMessage = "El volumen de efectos es obligatorio")]
        [Range(0, 100, ErrorMessage = "Debe estar entre 0 y 100")]
        public int VolumenEfectos { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
