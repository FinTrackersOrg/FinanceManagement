using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;
using System.Threading.Tasks;

namespace FinanceManagementSystem.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUser();
        Task<User> GetUserById(int id);
        Task<User> AddUser(UserDto userDto);
        Task<User> UpdateUser(int id, UserDto userDto);
        Task<bool> DeleteUser(int id);
    }
}
