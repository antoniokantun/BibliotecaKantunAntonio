using BibliotecaKantunAntonio.Models.Domain;

namespace BibliotecaKantunAntonio.Services.IServices
{
    public interface IRolServices
    {
        public Task<List<Rol>> GetAll();
    }
}
