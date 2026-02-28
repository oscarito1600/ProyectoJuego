using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class Ajuste
{
    public int AjustesId { get; set; }

    public int UsuarioId { get; set; }

    public int VolumenGeneral { get; set; }

    public int VolumenMusica { get; set; }

    public int VolumenEfectos { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
