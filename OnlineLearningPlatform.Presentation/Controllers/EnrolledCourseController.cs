using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.EnrollCourseDTOs;
using OnlineLearningPlatform.Presentation.DTOs.EnrolledCourseDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    [Authorize]
    public class EnrolledCourseController : ControllerBase
    {
        private readonly IEnrolledCourseService _service;
        private readonly IMapper _mapper;

        public EnrolledCourseController(IEnrolledCourseService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("{courseId:int}")]
        public async Task<IActionResult> Enroll(int courseId)
        {
            int userId = User.GetUserId();

            await _service.EnrollAsync(userId, courseId);
            return Ok("Enrolled successfully");
        }

        [Authorize(Roles = "Student")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyEnrollments()
        {
            int userId = User.GetUserId();

            var enrollments = await _service.GetUserEnrollmentsAsync(userId);
            return Ok(_mapper.Map<IEnumerable<EnrolledCourseResponseDTO>>(enrollments));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserEnrollments(int userId)
        {
            var enrollments = await _service.GetUserEnrollmentsAsync(userId);
            return Ok(_mapper.Map<IEnumerable<EnrolledCourseResponseDTO>>(enrollments));
        }
    }
}
