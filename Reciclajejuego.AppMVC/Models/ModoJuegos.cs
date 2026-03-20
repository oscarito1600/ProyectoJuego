using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class ModoJuegos
{
    public int ModoJuegoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual DetallesModo? DetallesModo { get; set; }

    public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();
}
