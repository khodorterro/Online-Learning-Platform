using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.QuizDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [ApiController]
    [Route("api/quizzes")]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService, IMapper mapper)
        {
            _mapper = mapper;
            _quizService = quizService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            if (quiz == null)
                return NotFound("Quiz not found");

            return Ok(_mapper.Map<QuizResponseDTO>(quiz));
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var quizzes = await _quizService.GetByCourseIdAsync(courseId,userId,role);

            return Ok(_mapper.Map<IEnumerable<QuizResponseDTO>>(quizzes));
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuizDTO dto)
        {
            int instructorId = User.GetUserId();

            var quiz = await _quizService.CreateAsync(dto.CourseId,dto.LessonId,dto.Title,
                dto.PassingScore,dto.TimeLimit,instructorId);

            return CreatedAtAction(nameof(GetById),new { id = quiz.Id },_mapper.Map<QuizResponseDTO>(quiz));
        }

        [Authorize(Roles = "Instructor")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateQuizDTO dto)
        {
            int instructorId = User.GetUserId();
            var quiz = await _quizService.UpdateAsync(id,dto.Title,dto.PassingScore,dto.TimeLimit,instructorId);

            if (quiz == null)
                return NotFound("Quiz not found");

            return Ok(_mapper.Map<QuizResponseDTO>(quiz));
        }

        [Authorize(Roles = "Instructor")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            int instructorId = User.GetUserId();
            await _quizService.DeleteAsync(id, instructorId);
            return NoContent();
        }
    }
}

