using BibliotecaKantunAntonio.Context;
using BibliotecaKantunAntonio.Models.Domain;
using BibliotecaKantunAntonio.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaKantunAntonio.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDBContext _context;
        public RolServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetAll()
        {
            List<Rol> result = await _context.Roles.ToListAsync();
            return result;
        }

    }
}
