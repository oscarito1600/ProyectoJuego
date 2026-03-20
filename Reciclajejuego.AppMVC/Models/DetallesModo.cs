using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class DetallesModo
{
    public int ModoJuegoId { get; set; }

    public int? Vidas { get; set; }

    public int? TiempoLimite { get; set; }

    public int? ComboMaximo { get; set; }

    public virtual ModoJuegos ModoJuego { get; set; } = null!;
}
