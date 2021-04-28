using System.Threading.Tasks;
using PocDotNet5.Api.Domain.Entities;

namespace PocDotNet5.Api.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindAsync(int id);
        Task AddAsync(User user);
    }
}