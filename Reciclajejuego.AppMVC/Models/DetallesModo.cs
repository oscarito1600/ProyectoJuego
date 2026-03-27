using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reciclajejuego.AppMVC.Models;

[Table("DetallesModoJuego")]
public partial class DetallesModo // ✅ CORREGIDO
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ModoJuegoId { get; set; }

    [Range(0, 10, ErrorMessage = "Las vidas deben estar entre 0 y 10")]
    public byte? Vidas { get; set; }

    [Range(0, 300, ErrorMessage = "El tiempo debe estar entre 0 y 300")]
    public int? TiempoLimite { get; set; }

    [Range(0, 50, ErrorMessage = "El combo máximo debe estar entre 0 y 50")]
    public int? ComboMaximo { get; set; }

    public virtual ModoJuego ModoJuego { get; set; } = null!;
}