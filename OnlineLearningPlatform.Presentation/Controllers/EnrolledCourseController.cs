using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.EnrollCourseDTOs;
using OnlineLearningPlatform.Presentation.DTOs.EnrolledCourseDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/EnrolledCourse")]
    [ApiController]
    public class EnrolledCourseController : ControllerBase
    {
        private readonly IEnrolledCourseService _service;
        private readonly IMapper _mapper;

        public EnrolledCourseController(
            IEnrolledCourseService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollCourseDTO dto)
        {
            await _service.EnrollAsync(dto.UserId, dto.CourseId);
            return Ok("Enrolled successfully");
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserEnrollments(int userId)
        {
            var enrollments = await _service.GetUserEnrollmentsAsync(userId);
            return Ok(_mapper.Map<IEnumerable<EnrolledCourseResponseDTO>>(enrollments));
        }
    }
}
