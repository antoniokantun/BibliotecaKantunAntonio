﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaKantunAntonio.Models.Domain
{
    public class Usuario
    {
        [Key]
        public int PkUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("Roles")]
        public int FkRol { get; set; }

        public Rol Roles { get; set; }
    }
}
