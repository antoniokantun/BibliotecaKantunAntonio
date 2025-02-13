using BibliotecaKantunAntonio.Models.Domain;

namespace BibliotecaKantunAntonio.Services.IServices
{
    public interface IUsuarioServices
    {
        public List<Usuario> ObtenerUsuarios();

        public bool CrearUsuario(Usuario request);

        public Usuario ObtenerUsuario(int id);
    }
}
