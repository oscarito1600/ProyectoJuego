using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres.")]
        [Display(Name = "Nombre del Rol")]
        public string Nombre { get; set; } = null!;

        // Relación inversa: Un rol puede pertenecer a muchos usuarios
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
