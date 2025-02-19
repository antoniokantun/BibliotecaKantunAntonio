using BibliotecaKantunAntonio.Context;
using BibliotecaKantunAntonio.Models.Domain;
using BibliotecaKantunAntonio.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BibliotecaKantunAntonio.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDBContext _context;
        public UsuarioServices(ApplicationDBContext context) 
        {
            _context = context;
        }
        public async Task<List<Usuario>>ObtenerUsuarios()
        {
            try
            {
                var result = await _context.Usuarios.Include(x => x.Roles).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error" + ex.Message);
            }
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(x => x.Roles)
                                                          .FirstOrDefaultAsync(x => x.PkUsuario == id);

                return usuario;

                //Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.PkUsuario == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error" + ex.Message);
            }
        }


        public async Task<bool> CrearUsuario(Usuario request)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    UserName = request.UserName,
                    Password = request.Password,
                    FkRol = request.FkRol,
                };

                _context.Usuarios.Add(usuario);
                var result = await _context.SaveChangesAsync();


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


        public async Task<bool> ActualizarUsuario(Usuario request)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.PkUsuario == request.PkUsuario);

                if (usuario == null)
                    return false;

                // Actualizar los campos
                usuario.Nombre = request.Nombre;
                usuario.Apellido = request.Apellido;
                usuario.UserName = request.UserName;
                usuario.FkRol = request.FkRol;

                // Solo actualizar la contraseña si se proporciona una nueva
                if (!string.IsNullOrEmpty(request.Password))
                {
                    usuario.Password = request.Password;
                }

                _context.Update(usuario);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar usuario: {ex.Message}");
            }
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                    return false;

                _context.Usuarios.Remove(usuario);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch(Exception ex)
            {
                throw new Exception("Hubo un error al eliminar: " + ex.Message);
            }
        }

    }
}
