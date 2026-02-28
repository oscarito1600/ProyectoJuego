using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class Contenedor
{
    public int ContenedorId { get; set; }

    public string TipoReciclaje { get; set; } = null!;

    public string Color { get; set; } = null!;

    public virtual ICollection<Residuo> Residuos { get; set; } = new List<Residuo>();
}
