using System;
using System.Collections.Generic;

namespace Reciclajejuego.AppMVC.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int MejorPuntaje { get; set; }

    public bool CuentaGoogle { get; set; }

    public virtual Ajuste? Ajuste { get; set; }

    public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();
}
