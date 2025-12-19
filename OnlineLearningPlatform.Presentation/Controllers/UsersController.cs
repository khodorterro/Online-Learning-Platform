using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
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
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserResponseDTO>>(users));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(_mapper.Map<UserResponseDTO>(user));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            var user = await _userService.RegisterAsync(
                dto.FullName,
                dto.Email,
                dto.Password,
                dto.Role
            );

            return CreatedAtAction(nameof(GetById),
                new { id = user.Id },
                _mapper.Map<UserResponseDTO>(user));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                User = _mapper.Map<UserResponseDTO>(user)
            });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            int userId = User.GetUserId();
            var user = await _userService.GetByIdAsync(userId);

            return Ok(_mapper.Map<UserResponseDTO>(user));
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile(string FullName)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();
            var updatedUser = await _userService.UpdateAsync(userId, FullName,role);

            return Ok(_mapper.Map<UserResponseDTO>(updatedUser));
        }

        [Authorize (Roles="Admin")]
        [HttpPut("User")]
        public async Task<IActionResult> UpdateUserProfile(int id,UpdateUserDTO dto)
        {
            int ID = id;
            var updatedUser = await _userService.UpdateAsync(ID, dto.FullName, dto.Role);

            return Ok(_mapper.Map<UserResponseDTO>(updatedUser));
        }
        [Authorize]
        [HttpPut("me/change-password")]
        public async Task<IActionResult> ChangeMyPassword(ChangePasswordDTO dto)
        {
            int userId = User.GetUserId();

            var success = await _userService.ChangePasswordAsync(
                userId,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!success)
                return BadRequest("Current password is incorrect.");

            return Ok("Password changed successfully.");
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteUser(id);
            if (!deleted)
                return NotFound("User not found.");

            return NoContent();
        }
    }
}
