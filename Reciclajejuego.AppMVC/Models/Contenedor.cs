using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    [Table("Contenedores")]
    public partial class Contenedor
    {
        public Contenedor()
        {
            Residuos = new HashSet<Residuo>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID del Contenedor")]
        public int ContenedorId { get; set; }

        [Required(ErrorMessage = "El tipo de reciclaje es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "No puede estar vacío o contener solo espacios")]
        [Display(Name = "Tipo de Reciclaje")]
        public string TipoReciclaje { get; set; } = string.Empty;

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 20 caracteres")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El color no puede estar vacío o contener solo espacios")]
        [Display(Name = "Color del Contenedor")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Lista de Residuos")]
        public virtual ICollection<Residuo> Residuos { get; set; }
    }
}
