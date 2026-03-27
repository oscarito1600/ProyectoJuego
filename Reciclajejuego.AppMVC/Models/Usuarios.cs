using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [StringLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255)]
        public string Contrasena { get; set; } = string.Empty;

        public bool EsCuentaGoogle { get; set; } = false;

        [Required]
        public int RolId { get; set; }

        public byte Estado { get; set; } = 1;

        // 🔗 RELACIONES
        [ForeignKey("RolId")]
        public virtual Rol? Rol { get; set; }

        public virtual Ajuste? Ajuste { get; set; }

        public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();

        public virtual ICollection<MejorPuntaje> MejoresPuntajes { get; set; } = new List<MejorPuntaje>();

        public virtual ICollection<RecuperacionContrasenas> RecuperacionContrasenas { get; set; } = new List<RecuperacionContrasenas>();
    }
}