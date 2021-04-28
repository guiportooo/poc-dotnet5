namespace PocDotNet5.Api.Repositories
{
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Repositories;
    using EntityFramework;

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