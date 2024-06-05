using Microsoft.EntityFrameworkCore;
using tsm_back.Data;
using tsm_back.Models;

namespace tsm_back.Repositories
{
    public class UserRepository
    {
        private readonly TSMContext _context;

        public UserRepository(TSMContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUser(string login) 
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task CreateUser(User user)
        {
            if (_context.Users.Any(p => p.Login == user.Login))
                throw new Exception("Такой логин используется!");
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserName(int userID, string name)
        {
            var user = _context.Users.First(p => p.Id == userID);
            user.Nickname = name;

            await _context.SaveChangesAsync();
        }
    }
}
