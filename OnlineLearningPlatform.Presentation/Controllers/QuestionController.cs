using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.QuestionDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/questions")]
    [ApiController]
    [Authorize(Roles = "Instructor")] 
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService,IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            int instructorId = User.GetUserId();

            var question = await _questionService.GetByIdWithOwnershipAsync(id, instructorId);

            return Ok(_mapper.Map<QuestionResponseDTO>(question));
        }

        [HttpGet("quiz/{quizId:int}")]
        public async Task<IActionResult> GetByQuiz(int quizId)
        {
            int instructorId = User.GetUserId();

            var questions = await _questionService.GetByQuizIdWithOwnershipAsync(quizId, instructorId);

            return Ok(_mapper.Map<IEnumerable<QuestionResponseDTO>>(questions));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionDTO dto)
        {
            int instructorId = User.GetUserId();

            var question = await _questionService.CreateAsync(dto.QuizId,dto.QuestionText,dto.QuestionType,instructorId);

            return CreatedAtAction(nameof(GetById),new { id = question.Id },_mapper.Map<QuestionResponseDTO>(question));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateQuestionDTO dto)
        {
            int instructorId = User.GetUserId();

            var question = await _questionService.UpdateAsync(id,dto.QuestionText,dto.QuestionType,instructorId);

            return Ok(_mapper.Map<QuestionResponseDTO>(question));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            int instructorId = User.GetUserId();

            await _questionService.DeleteAsync(id, instructorId);
            return NoContent();
        }
    }
}
