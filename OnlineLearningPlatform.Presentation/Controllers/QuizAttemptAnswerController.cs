using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.BusinessLayer.Services;
using OnlineLearningPlatform.Presentation.DTOs.QuizAttemptAnswerDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/QuizAttemptAnswer")]
    [ApiController]
    public class QuizAttemptAnswerController : ControllerBase
    {
        private readonly IQuizAttemptAnswerService _service;
        private readonly IQuizAttemptService _quizAttemptService;
        private readonly IMapper _mapper;

        public QuizAttemptAnswerController(IQuizAttemptAnswerService service,IMapper mapper,IQuizAttemptService quizAttemptService)
        {
            _service = service;
            _mapper = mapper;
            _quizAttemptService = quizAttemptService;
        }

        [HttpGet("attempt/{attemptId:int}")]
        public async Task<IActionResult> GetByAttempt(int attemptId)
        {
            var answers = await _service.GetByAttemptIdAsync(attemptId);
            return Ok(_mapper.Map<IEnumerable<QuizAttemptAnswerResponseDTO>>(answers));
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(SubmitQuizAttemptAnswersDTO dto)
        {
            await _service.SubmitAnswersAsync(
                dto.AttemptId,
                dto.Answers.Select(a => (a.QuestionId, a.SelectedAnswerId))
            );

            bool passed = await _quizAttemptService.IsPassedAsync(dto.AttemptId);

            return Ok(new
            {
                Message = "Quiz submitted successfully",
                Passed = passed
            });
        }


        // GET: api/quiz-attempts/{attemptId}/questions
        [HttpGet("{attemptId:int}/questions")]
        public async Task<IActionResult> GetQuestionsByAttempt(int attemptId)
        {
            var questions = await _service
                .GetQuestionsByAttemptAsync(attemptId);

            var result = _mapper.Map<IEnumerable<AttemptQuestionDTO>>(questions);

            return Ok(result);
        }


    }
}
