using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(_mapper.Map<UserResponseDTO>(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            var user = await _userService.RegisterAsync(
                dto.FullName,
                dto.Email,
                dto.Password,
                dto.Role
            );

            var response = _mapper.Map<UserResponseDTO>(user);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            return Ok(_mapper.Map<UserResponseDTO>(user));
        }

        [HttpPut("{id:int}/Update")]
        public async Task<IActionResult> Update(int id, UpdateUserDTO dto)
        {
            var updatedUser = await _userService.UpdateAsync(id, dto.FullName, dto.Role);
            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(_mapper.Map<UserResponseDTO>(updatedUser));
        }

        [HttpDelete("{id:int}/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteUser(id);
            if (!deleted)
                return NotFound("User not found.");

            return NoContent();
        }

        [HttpPut("{id:int}/change-password")]
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
