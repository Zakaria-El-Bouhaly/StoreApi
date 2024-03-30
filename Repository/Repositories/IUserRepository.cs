using Shared.Models;

namespace Repository.Repositories
{
    public interface IUserRepository
    {
        
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> AddUser(User user);
        Task<User?> UpdateUser(User user);
        Task<User?> DeleteUser(int id);

    }
}
