using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.CourseCategoryDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/course-categories")]
    [ApiController]
    [Authorize]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly IMapper _mapper;
        public CourseCategoryController(ICourseCategoryService courseCategoryService,IMapper mapper)
        {
            _courseCategoryService = courseCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _courseCategoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CourseCategoryResponseDTO>>(categories));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _courseCategoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Course category not found.");

            return Ok(_mapper.Map<CourseCategoryResponseDTO>(category));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseCategoryDTO dto)
        {

            var category = await _courseCategoryService.CreateAsync(dto.Name, dto.Description);


            return CreatedAtAction(nameof(GetById),new { id = category.Id },_mapper.Map<CourseCategoryResponseDTO>(category));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCourseCategoryDTO dto)
        {
            var category = await _courseCategoryService.UpdateAsync(id, dto.Name, dto.Description);
            if (category == null)
                return NotFound("Course category not found.");

            return Ok(_mapper.Map<CourseCategoryResponseDTO>(category));

        }

    }
}
