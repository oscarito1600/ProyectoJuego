using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    [Table("ModosJuego")]
    public partial class ModoJuego
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID del Modo de Juego")]
        public int Id { get; set; } // ✅ CORREGIDO

        [Required(ErrorMessage = "El nombre del modo de juego es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El nombre no puede estar vacío o contener solo espacios")]
        [Display(Name = "Nombre del Modo de Juego")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Detalles del Modo")]
        public virtual ICollection<DetallesModo> DetallesModo { get; set; } = new List<DetallesModo>(); // ✅ CORREGIDO

        [Display(Name = "Lista de Juegos")]
        public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();

        public virtual ICollection<MejorPuntaje> MejoresPuntajes { get; set; } = new List<MejorPuntaje>(); // ✅ IMPORTANTE
    }
}