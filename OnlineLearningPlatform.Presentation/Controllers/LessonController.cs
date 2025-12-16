using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearningPlatform.Presentation.DTOs.LessonDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IlessonService _lessonService;
        private readonly IMapper _mapper;
        public LessonController(IlessonService lessonService, IMapper mapper)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourseId(int courseId)
        {
            var lessons = await _lessonService.GetByCourseIdAsync(courseId);

            var response = _mapper.Map<IEnumerable<LessonResponseDTO>>(lessons);
            return Ok(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null) return NotFound();

            return Ok(_mapper.Map<LessonResponseDTO>(lesson));
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(CreateLessonDTO dtO)
        {
            var lesson = await _lessonService.AddAsync(dtO.CourseId, dtO.Title, dtO.Content,
                dtO.VideoUrl, dtO.Order, dtO.EstimatedDuration);

            var result = _mapper.Map<LessonResponseDTO>(lesson);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateLesson(int id,UpdateLessonDTO dto)
        {
            var lesson= await _lessonService.UpdateAsync(id, dto.Title, dto.Content,
                dto.VideoUrl, dto.Order, dto.EstimatedDuration);

            if(lesson == null)
                return NotFound();

            var result=_mapper.Map<LessonResponseDTO>(lesson);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var isdeleted=await _lessonService.DeleteAsync(id);
            if(isdeleted ==false)
                 return NotFound();
            return Ok("Lesson deleted Succesfully");
        }
    }
}
