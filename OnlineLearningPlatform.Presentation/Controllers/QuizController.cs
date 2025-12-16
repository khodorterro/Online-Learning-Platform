using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.QuizDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService,IMapper mapper)
        {
            _mapper = mapper;
            _quizService = quizService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            return Ok(_mapper.Map<QuizResponseDTO>(quiz));
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var quizzes = await _quizService.GetByCourseIdAsync(courseId);
            return Ok(_mapper.Map<IEnumerable<QuizResponseDTO>>(quizzes));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuizDTO dto)
        {
            var quiz = await _quizService.CreateAsync(
                dto.CourseId,
                dto.LessonId,
                dto.Title,
                dto.PassingScore,
                dto.TimeLimit
            );

            var response = _mapper.Map<QuizResponseDTO>(quiz);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateQuizDTO dto)
        {
            var quiz = await _quizService.UpdateAsync(
                id,
                dto.Title,
                dto.PassingScore,
                dto.TimeLimit
            );

            return Ok(_mapper.Map<QuizResponseDTO>(quiz));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _quizService.DeleteAsync(id);
            return NoContent();
        }
    }
}

