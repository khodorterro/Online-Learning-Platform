using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.LessonCompletionDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/LessonCompletion")]
    [ApiController]
    public class LessonCompletionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILessonCompletionService _service;
        public LessonCompletionController(IMapper mapper, ILessonCompletionService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CompleteLesson(CompleteLessonDTO dto)
        {
            await _service.CompleteLessonAsync(dto.UserId, dto.LessonId);
            return Ok("Lesson marked as completed");
        }


        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetCompletedLessons(int userId)
        {
            var lessons = await _service.GetCompletedLessonsAsync(userId);
            return Ok(_mapper.Map<IEnumerable<LessonCompletionResponseDTO>>(lessons));
        }
    }
}
