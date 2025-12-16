using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.AnswerDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Answer")]
    [ApiController]
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnswerDTO dto)
        {
            var answer = await _answerService.CreateAsync(dto.QuestionId,dto.AnswerText,dto.IsCorrect);

            var response = _mapper.Map<AnswerResponseDTO>(answer);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateAnswerDTO dto)
        {
            var answer = await _answerService.UpdateAsync(id,dto.AnswerText,dto.IsCorrect);

            return Ok(_mapper.Map<AnswerResponseDTO>(answer));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _answerService.DeleteAsync(id);
            return NoContent();
        }
    }
}

