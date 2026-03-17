using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class ModoJuego
    {
        public int ModoJuegoId { get; set; }

        [Required(ErrorMessage = "El nombre del modo de juego es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; } = null!;

        public virtual DetallesModo? DetallesModo { get; set; }

        public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();
    }
}