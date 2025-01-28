using BibliotecaKantunAntonio.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaKantunAntonio.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        } 
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Antonio",
                    Apellido = "Kantun",
                    UserName = "antonio.kantun",
                    Password = "123456",
                    FkRol = 1
                }
                );

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRol = 1,
                    Nombre = "Administrador"
                }
                );
        }
    }
}
