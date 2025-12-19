using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.LessonCompletionDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/lesson-completions")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class LessonCompletionController : ControllerBase
    {

        private readonly ILessonCompletionService _service;
        private readonly IMapper _mapper;
        public LessonCompletionController(ILessonCompletionService service,IMapper mapper)
        {

            _service = service;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CompleteLesson(CompleteLessonDTO dto)
        {
            int userId = User.GetUserId();

            await _service.CompleteLessonAsync(userId,dto.LessonId);

            return Ok("Lesson marked as completed");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyCompletedLessons()
        {
            int userId = User.GetUserId();
            var lessons = await _service.GetCompletedLessonsAsync(userId);

            return Ok(_mapper.Map<IEnumerable<LessonCompletionResponseDTO>>(lessons));
        }
    }
}
