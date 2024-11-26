using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagementSystem.IServices.Services
{
    public class UserService:IUserService
    {
        private readonly FinanceDBContext _dbContext;

        public UserService(FinanceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

       async Task<User> IUserService.AddUser(UserDto userDto)
        {
            User user = new User();
            user.UserId = userDto.UserId;
            user.RoleId = userDto.RoleId;
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PasswordHash = userDto.PasswordHash;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        async Task<bool> IUserService.DeleteUser(int id)
        {

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        async Task<IEnumerable<User>> IUserService.GetUser()
        {

            return await _dbContext.Users.ToListAsync();
        }

       async Task<User> IUserService.GetUserById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        async Task<User> IUserService.UpdateUser(int id, UserDto userDto)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user== null)
          
            user.UserId = userDto.UserId;
            user.RoleId = userDto.RoleId;
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PasswordHash = userDto.PasswordHash;

            //_dbContext.Users.Update(existingUser);

            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
