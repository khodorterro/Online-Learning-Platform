using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.QuizAttemptDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [ApiController]
    [Route("api/quiz-attempts")]
    [Authorize] 
    public class QuizAttemptController : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService;
        private readonly IMapper _mapper;

        public QuizAttemptController(
            IQuizAttemptService quizAttemptService,
            IMapper mapper)
        {
            _quizAttemptService = quizAttemptService;
            _mapper = mapper;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var attempt = await _quizAttemptService.GetByIdAsync(id);

            if (role == "Student" && attempt.UserId != userId)
                return Forbid();

            return Ok(_mapper.Map<QuizAttemptResponseDTO>(attempt));
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetMyAttempts()
        {
            int userId = User.GetUserId();
            var attempts = await _quizAttemptService.GetByUserIdAsync(userId);

            return Ok(_mapper.Map<IEnumerable<QuizAttemptResponseDTO>>(attempts));
        }

        [Authorize(Roles = "Instructor,Admin")]
        [HttpGet("quiz/{quizId:int}")]
        public async Task<IActionResult> GetByQuiz(int quizId)
        {
            var attempts = await _quizAttemptService.GetByQuizIdAsync(quizId);
            return Ok(_mapper.Map<IEnumerable<QuizAttemptResponseDTO>>(attempts));
        }


        [Authorize(Roles = "Student")]
        [HttpPost("start")]
        public async Task<IActionResult> StartQuiz([FromBody] StartQuizAttemptDTO dto)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var attempt = await _quizAttemptService.CreateAsync(dto.QuizId,userId,role);

            return Ok(_mapper.Map<QuizAttemptResponseDTO>(attempt));
        }

    }
}
