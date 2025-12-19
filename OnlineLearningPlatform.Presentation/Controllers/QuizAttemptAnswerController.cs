using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;

using OnlineLearningPlatform.Presentation.DTOs.QuizAttemptAnswerDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/quiz-attempt-answers")]
    [ApiController]
    [Authorize]
    public class QuizAttemptAnswerController : ControllerBase
    {
        private readonly IQuizAttemptAnswerService _service;
        private readonly IQuizAttemptService _quizAttemptService;
        private readonly IMapper _mapper;

        public QuizAttemptAnswerController(
            IQuizAttemptAnswerService service,
            IMapper mapper,
            IQuizAttemptService quizAttemptService)
        {
            _service = service;
            _mapper = mapper;
            _quizAttemptService = quizAttemptService;
        }


        [HttpGet("attempt/{attemptId:int}")]
        public async Task<IActionResult> GetByAttempt(int attemptId)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var attempt = await _quizAttemptService.GetByIdAsync(attemptId);


            if (role == "Student" && attempt.UserId != userId)
                return Forbid();

            var answers = await _service.GetByAttemptIdAsync(attemptId);
            return Ok(_mapper.Map<IEnumerable<QuizAttemptAnswerResponseDTO>>(answers));
        }

        [Authorize(Roles = "Student")]
        [HttpPost("submit")]

        public async Task<IActionResult> Submit(
            SubmitQuizAttemptAnswersDTO dto)
        {
            int userId = User.GetUserId();

            var attempt = await _quizAttemptService.GetByIdAsync(dto.AttemptId);

            if (attempt.UserId != userId)
                return Forbid();

            await _service.SubmitAnswersAsync(dto.AttemptId,dto.Answers.Select(a => (a.QuestionId, a.SelectedAnswerId)));

            bool passed = await _quizAttemptService.IsPassedAsync(dto.AttemptId);

            return Ok(new
            {
                Message = "Quiz submitted successfully",
                Passed = passed
            });
        }



        [HttpGet("{attemptId:int}/questions")]
        public async Task<IActionResult> GetQuestionsByAttempt(int attemptId)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var attempt = await _quizAttemptService.GetByIdAsync(attemptId);

            if (role == "Student" && attempt.UserId != userId)
                return Forbid();

            var questions = await _service
                .GetQuestionsByAttemptAsync(attemptId);
            return Ok(_mapper.Map<IEnumerable<AttemptQuestionDTO>>(questions));
        }


    }
}
