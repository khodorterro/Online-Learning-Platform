using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/CourseProgress")]
    [ApiController]
    [Authorize]
    public class CourseProgressController : ControllerBase
    {
        private readonly ICourseProgressService _service;

        public CourseProgressController(ICourseProgressService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet("{courseId:int}/user/{userId:int}")]
        public async Task<IActionResult> GetProgress(int courseId, int userId)
        {
            int progress = await _service.GetCourseProgressAsync(userId, courseId);

            return Ok(new CourseProgressResponseDTO
            {
                CourseId = courseId,
                ProgressPercentage = progress
            });
        }
        [Authorize(Roles = "Instructor,Admin")]
        [HttpGet("{courseId:int}/student/{studentId:int}")]
        public async Task<IActionResult> GetStudentProgress(int courseId,int studentId)
        {
            int requesterId = User.GetUserId();
            string role = User.GetRole();

            int progress = await _service.GetStudentProgressAsync(courseId,studentId,requesterId,role);

            return Ok(new CourseProgressResponseDTO
            {
                CourseId = courseId,
                ProgressPercentage = progress
            });
        }
    }
}
