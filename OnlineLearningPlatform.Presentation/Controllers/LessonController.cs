using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.BusinessLayer.Services;
using OnlineLearningPlatform.Presentation.DTOs.LessonDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    [Authorize] 
    public class LessonController : ControllerBase
    {
        private readonly IlessonService _lessonService;
        private readonly IMapper _mapper;

        public LessonController(IlessonService lessonService,IMapper mapper)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetLessonsByCourse(int courseId)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var lessons = await _lessonService.GetByCourseIdAsync(courseId, userId, role);

            return Ok(_mapper.Map<IEnumerable<LessonResponseDTO>>(lessons));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var lesson = await _lessonService.GetByIdWithAccessAsync(id, userId, role);

            return Ok(_mapper.Map<LessonResponseDTO>(lesson));
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> AddLesson(CreateLessonDTO dto)
        {
            int instructorId = User.GetUserId();

            var lesson = await _lessonService.AddAsync(dto.CourseId,dto.Title,dto.Content,dto.VideoUrl
                ,dto.Order,dto.EstimatedDuration,instructorId);

            return CreatedAtAction(nameof(GetById),new { id = lesson.Id },_mapper.Map<LessonResponseDTO>(lesson));
        }

        [Authorize(Roles = "Instructor")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLesson(
            int id,
            UpdateLessonDTO dto)
        {
            int instructorId = User.GetUserId();

            var lesson = await _lessonService.UpdateAsync(id,dto.Title,dto.Content,dto.VideoUrl
                ,dto.Order,dto.EstimatedDuration,instructorId);

            return Ok(_mapper.Map<LessonResponseDTO>(lesson));
        }

        [Authorize(Roles = "Instructor")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            int instructorId = User.GetUserId();

            await _lessonService.DeleteAsync(id, instructorId);
            return NoContent();
        }
    }
}
