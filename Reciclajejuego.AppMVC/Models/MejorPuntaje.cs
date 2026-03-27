using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models;

[Table("MejoresPuntajes")]
public partial class MejorPuntaje
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El modo de juego es obligatorio")]
    public int ModoJuegoId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El puntaje no puede ser negativo")]
    public int Puntaje { get; set; } = 0;

    public DateTime FechaAlcanzado { get; set; } = DateTime.Now;

    // Relaciones
    public virtual Usuarios Usuario { get; set; } = null!;
    public virtual ModoJuego ModoJuego { get; set; } = null!;
}