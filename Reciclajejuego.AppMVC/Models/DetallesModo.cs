using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class DetallesModo
{
    public int Id { get; set; }

    public int ModoJuegoId { get; set; }

    public byte? Vidas { get; set; }

    public int? TiempoLimite { get; set; }

    public int? ComboMaximo { get; set; }

    public virtual ModoJuego ModoJuego { get; set; } = null!;
}