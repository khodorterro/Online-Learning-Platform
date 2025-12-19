using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs;
using OnlineLearningPlatform.Presentation.DTOs.QuizDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Review")]
    [Authorize]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("{courseId:int}")]
        public async Task<IActionResult> Create(
            int courseId,
            CreateReviewDTO dto)
        {
            int userId = User.GetUserId();

            var review = await _service.CreateAsync(
                userId,
                courseId,
                dto.Rating,
                dto.Comment
            );

            return Ok(_mapper.Map<ReviewResponseDTO>(review));
        }

        [AllowAnonymous]
        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var reviews = await _service.GetByCourseIdAsync(courseId);
            return Ok(_mapper.Map<IEnumerable<ReviewResponseDTO>>(reviews));
        }
    }
}
