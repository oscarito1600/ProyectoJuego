using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class Contenedor
    {
        public int ContenedorId { get; set; }

        [Required(ErrorMessage = "El tipo de reciclaje es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string TipoReciclaje { get; set; } = null!;

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Color { get; set; } = null!;

        public virtual ICollection<Residuo> Residuos { get; set; } = new List<Residuo>();
    }
}
