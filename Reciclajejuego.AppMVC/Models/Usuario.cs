using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        [StringLength(150)]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;

        [Display(Name = "Cuenta de Google")]
        public bool EsCuentaGoogle { get; set; } = false;

        [Required(ErrorMessage = "El rol es obligatorio")]
        public int RolId { get; set; }

        [Range(0, 1, ErrorMessage = "El estado debe ser 0 o 1")]
        public byte Estado { get; set; } = 1;

        // Relaciones
        [ForeignKey("RolId")]
        public virtual Rol? Rol { get; set; }

        public virtual Ajuste? Ajuste { get; set; }

        public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();

        public virtual ICollection<MejoresPuntaje> MejoresPuntajes { get; set; } = new List<MejoresPuntaje>();
    }
}