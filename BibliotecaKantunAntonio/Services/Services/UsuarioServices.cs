using BibliotecaKantunAntonio.Context;
using BibliotecaKantunAntonio.Models.Domain;
using BibliotecaKantunAntonio.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaKantunAntonio.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDBContext _context;
        public UsuarioServices(ApplicationDBContext context) 
        {
            _context = context;
        }
        public List<Usuario>ObtenerUsuarios()
        {
            try
            {
                var result = _context.Usuarios.Include(x => x.Roles).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error" + ex.Message);
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            try
            {
                Usuario usuario = _context.Usuarios.Find(id);

                return usuario;

                //Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.PkUsuario == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error" + ex.Message);
            }
        }


        public bool CrearUsuario(Usuario request)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = 1
                };

                _context.Usuarios.Add(usuario);
                var result = _context.SaveChanges();


                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error" + ex.Message);
            }
        }
    }
}
