using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.QuizAttemptDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [ApiController]
    [Route("api/quiz-attempts")]
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

        // GET: api/quiz-attempts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attempt = await _quizAttemptService.GetByIdAsync(id);
            return Ok(_mapper.Map<QuizAttemptResponseDTO>(attempt));
        }

        // GET: api/quiz-attempts/user/{userId}
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var attempts = await _quizAttemptService.GetByUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<QuizAttemptResponseDTO>>(attempts));
        }

        // GET: api/quiz-attempts/quiz/{quizId}
        [HttpGet("quiz/{quizId:int}")]
        public async Task<IActionResult> GetByQuiz(int quizId)
        {
            var attempts = await _quizAttemptService.GetByQuizIdAsync(quizId);
            return Ok(_mapper.Map<IEnumerable<QuizAttemptResponseDTO>>(attempts));
        }

        // POST: api/quiz-attempts/start
        [HttpPost("start")]
        public async Task<IActionResult> StartQuiz(StartQuizAttemptDTO dto)
        {
            var attempt = await _quizAttemptService.CreateAsync(
                dto.QuizId,
                dto.UserId
            );

            var response = _mapper.Map<QuizAttemptResponseDTO>(attempt);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response
            );
        }
    }
}
