using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/CourseProgress")]
    [ApiController]
    public class CourseProgressController : ControllerBase
    {
        private readonly ICourseProgressService _service;

        public CourseProgressController(ICourseProgressService service)
        {
            _service = service;
        }

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
    }
}
