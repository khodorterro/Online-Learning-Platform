using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> DeleteUser(int id);
        Task<User> RegisterAsync(string fullName, string email, string password, string role);
        Task<User?> AuthenticateAsync(string email, string password);
        Task<User?> UpdateAsync(int id, string fullName, string role);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}
