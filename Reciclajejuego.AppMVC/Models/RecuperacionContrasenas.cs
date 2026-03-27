using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    [Table("RecuperacionContrasena")]
    public class RecuperacionContrasenas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Codigo { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // ✅ Campo calculado en SQL
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaExpiracion { get; set; }

        public bool Usado { get; set; } = false;

        public virtual Usuarios Usuario { get; set; } = null!;
    }
}