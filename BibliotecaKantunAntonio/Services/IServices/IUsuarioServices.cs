using BibliotecaKantunAntonio.Models.Domain;

namespace BibliotecaKantunAntonio.Services.IServices
{
    public interface IUsuarioServices
    {
        Task<List<Usuario>> ObtenerUsuarios();
        Task<bool> CrearUsuario(Usuario request);
        Task<Usuario> ObtenerUsuario(int id);
        Task<bool> ActualizarUsuario(Usuario request);
        Task<bool> EliminarUsuario(int id);

    }
}
