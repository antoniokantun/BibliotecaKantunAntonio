using System.ComponentModel.DataAnnotations;

namespace BibliotecaKantunAntonio.Models.Domain
{
    public class Rol
    {
        [Key]
        public int PkRol { get; set; }
        public required string Nombre { get; set; }

    }
}
