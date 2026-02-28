using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class Residuo
{
    public int ResiduoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Imagen { get; set; }

    public int Puntos { get; set; }

    public int ContenedorId { get; set; }

    public virtual Contenedor Contenedor { get; set; } = null!;
}
