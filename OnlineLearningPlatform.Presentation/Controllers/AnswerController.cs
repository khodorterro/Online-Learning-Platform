using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.AnswerDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/answers")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;

        public AnswerController(IAnswerService answerService, IMapper mapper)
        {
            _answerService = answerService;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var answer = await _answerService.GetByIdAsync(id);
            return Ok(_mapper.Map<AnswerResponseDTO>(answer));
        }

        [HttpGet("question/{questionId:int}")]
        public async Task<IActionResult> GetByQuestion(int questionId)
        {
            var answers = await _answerService.GetByQuestionIdAsync(questionId);
            return Ok(_mapper.Map<IEnumerable<AnswerResponseDTO>>(answers));
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnswerDTO dto)
        {
            int instructorId = User.GetUserId();

            var answer = await _answerService.CreateAsync(dto.QuestionId,dto.AnswerText,dto.IsCorrect,instructorId);

            return CreatedAtAction(nameof(GetById),new { id = answer.Id },_mapper.Map<AnswerResponseDTO>(answer));
        }

        [Authorize(Roles = "Instructor")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateAnswerDTO dto)
        {
            int instructorId = User.GetUserId();

            var answer = await _answerService.UpdateAsync(id,dto.AnswerText,dto.IsCorrect,instructorId);

            return Ok(_mapper.Map<AnswerResponseDTO>(answer));
        }

        [Authorize(Roles = "Instructor")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            int instructorId = User.GetUserId();

            await _answerService.DeleteAsync(id, instructorId);
            return NoContent();
        }
    }
}
