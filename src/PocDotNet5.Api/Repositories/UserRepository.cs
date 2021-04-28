using System.Threading.Tasks;
using PocDotNet5.Api.Domain.Entities;
using PocDotNet5.Api.Domain.Repositories;
using PocDotNet5.Api.EntityFramework;

namespace PocDotNet5.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PocDotNet5Context _context;

        public UserRepository(PocDotNet5Context context) => _context = context;

        public async Task<User> FindAsync(int id) => await _context.Users.FindAsync(id);

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}