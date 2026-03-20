using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class Juego
{
    public int JuegoId { get; set; }

    public int UsuarioId { get; set; }

    public int ModoJuegoId { get; set; }

    public int PuntuacionActual { get; set; }

    public DateTime FechaInicio { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ModoJuegos ModoJuego { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
