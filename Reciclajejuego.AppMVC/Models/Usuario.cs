using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contrasena { get; set; } = null!;

        public string? CuentaGoogle { get; set; }

        public int MejorPuntaje { get; set; } // Propiedad simple para el puntaje

        public string? Rol { get; set; }

        public int? RolId { get; set; }

        [ForeignKey("RolId")]
        public virtual Rol? RolNavigation { get; set; }

        public virtual Ajuste? Ajuste { get; set; }

        public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();
    }
}