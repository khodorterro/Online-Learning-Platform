using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            var result = users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            });

            return Ok(result);
        }

        // GET: api/users/{id}
        [HttpGet("GetById{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(new UserResponseDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            });
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(
                    dto.FullName,
                    dto.Email,
                    dto.Password,
                    dto.Role
                );

                return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserResponseDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new UserResponseDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            });
        }
        [HttpDelete("DeleteById{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            var user=await _userService.GetByIdAsync(id);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            if(await _userService.DeleteUser(id)==true)
            {
                return Ok("User deleted successfuly");
            }
           return NotFound("error");
        }

        [HttpPut("UpdateUser{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateUserDTO dto)
        {
            var updatedUser = await _userService.UpdateAsync(id, dto.FullName, dto.Role);
            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(new UserResponseDTO
            {
                Id = updatedUser.Id,
                FullName = updatedUser.FullName,
                Email = updatedUser.Email,
                Role = updatedUser.Role,
                CreatedAt = updatedUser.CreatedAt
            });
        }


        [HttpPut("change-password{id:int}")]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDTO dto)
        {
            var success = await _userService.ChangePasswordAsync(
                id,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!success)
                return BadRequest("Current password is incorrect or user not found.");

            return Ok("Password changed successfully.");
        }

    }
}
