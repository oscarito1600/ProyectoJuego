using System;
using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models;

public partial class MejoresPuntaje
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    [Display(Name = "Usuario")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El modo de juego es obligatorio")]
    [Display(Name = "Modo de Juego")]
    public int ModoJuegoId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El puntaje no puede ser negativo")]
    [Display(Name = "Puntaje")]
    public int? Puntaje { get; set; } = 0;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha Alcanzada")]
    public DateTime? FechaAlcanzado { get; set; }

    // Relaciones
    public virtual Usuario Usuario { get; set; } = null!;

    public virtual ModoJuego ModoJuego { get; set; } = null!;
}