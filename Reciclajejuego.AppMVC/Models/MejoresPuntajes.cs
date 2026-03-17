using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models
{
    public partial class MejorPuntaje
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El modo de juego es obligatorio")]
        public int ModoJuegoId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El puntaje no puede ser negativo")]
        public int Puntaje { get; set; } = 0;

        [DataType(DataType.DateTime)]
        public DateTime FechaAlcanzado { get; set; } = DateTime.Now;

        public virtual Usuario Usuario { get; set; } = null!;

        public virtual ModoJuego ModoJuego { get; set; } = null!;
    }
}