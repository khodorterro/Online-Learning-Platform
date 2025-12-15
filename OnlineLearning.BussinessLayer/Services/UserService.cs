using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class UserService : IUserService
    {

       private readonly IUserRepository _userRepository;

       public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> RegisterAsync(string fullname,string email,string password,string role)
        {
            if(await _userRepository.GetByEmailAsync(email)!=null)
            {
                throw new Exception("Email already Exists");
            }
            var user=new User
            {
                FullName = fullname,
                Email = email,
                HashedPassword=PasswordHasher.Hash(password),
                Role = role,
                CreatedAt = DateTime.UtcNow,
            };
            
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return null;

            var isValid = PasswordHasher.Verify(password, user.HashedPassword);
            if (!isValid)
                return null;

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user=await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;
           await _userRepository.DeleteAsync(id);
           return true;
        }
        public async Task<User?> UpdateAsync(int id, string fullName, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            user.FullName = fullName;
            user.Role = role;

            await _userRepository.UpdateAsync(user);
            return user;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            if (!PasswordHasher.Verify(currentPassword, user.HashedPassword))
                return false;

            user.HashedPassword = PasswordHasher.Hash(newPassword);
            await _userRepository.UpdateAsync(user);

            return true;
        }

    }
}
