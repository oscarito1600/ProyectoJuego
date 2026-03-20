using System.ComponentModel.DataAnnotations;

namespace Reciclajejuego.AppMVC.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        public string Nombre { get; set; }
    }
}