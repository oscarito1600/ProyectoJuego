using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Ajuste
    {
        public int Id { get; set; } // ✅ CORREGIDO

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El volumen general es obligatorio")]
        [Range(0, 100, ErrorMessage = "Debe estar entre 0 y 100")]
        public byte VolumenGeneral { get; set; } // ⚠️ mejor byte (como en DB)

        [Required(ErrorMessage = "El volumen de música es obligatorio")]
        [Range(0, 100, ErrorMessage = "Debe estar entre 0 y 100")]
        public byte VolumenMusica { get; set; }

        [Required(ErrorMessage = "El volumen de efectos es obligatorio")]
        [Range(0, 100, ErrorMessage = "Debe estar entre 0 y 100")]
        public byte VolumenEfectos { get; set; }

        public virtual Usuarios Usuario { get; set; } = null!;
    }
}
