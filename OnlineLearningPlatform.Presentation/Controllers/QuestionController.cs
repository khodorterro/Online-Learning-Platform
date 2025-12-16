using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.QuestionDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;
        public QuestionController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _questionService.GetByIdAsync(id);
            return Ok(_mapper.Map<QuestionResponseDTO>(question));
        }

        [HttpGet("quiz/{quizId:int}")]
        public async Task<IActionResult> GetByQuiz(int quizId)
        {
            var questions = await _questionService.GetByQuizIdAsync(quizId);
            return Ok(_mapper.Map<IEnumerable<QuestionResponseDTO>>(questions));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionDTO dto)
        {
            var question = await _questionService.CreateAsync(
                dto.QuizId,
                dto.QuestionText,
                dto.QuestionType
            );

            var response = _mapper.Map<QuestionResponseDTO>(question);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateQuestionDTO dto)
        {
            var question = await _questionService.UpdateAsync(
                id,
                dto.QuestionText,
                dto.QuestionType
            );

            return Ok(_mapper.Map<QuestionResponseDTO>(question));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
