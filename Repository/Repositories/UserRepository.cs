using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Shared.Models;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<User?> DeleteUser(int id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }


        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }



        public async Task<User?> UpdateUser(User user)
        {
            // find the user by id
            var userExists = await GetUserById(user.Id);
            if (userExists == null)
            {
                return null;
            }
            user.FullName = user.FullName;
            user.Email = user.Email;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
