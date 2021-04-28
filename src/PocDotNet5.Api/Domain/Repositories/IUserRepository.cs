namespace PocDotNet5.Api.Domain.Repositories
{
    using System.Threading.Tasks;
    using Entities;

    public interface IUserRepository
    {
        Task<User?> FindAsync(int id);
        Task AddAsync(User user);
    }
}