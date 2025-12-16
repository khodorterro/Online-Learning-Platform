using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearningPlatform.Presentation.DTOs.CourseCategoryDTOs;
using System.Runtime.InteropServices;
using OnlineLearningPlatform.Presentation.Mapping;
using AutoMapper;


namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Coursecategory")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService;

        private readonly IMapper _mapper;
        public CourseCategoryController(ICourseCategoryService courseCategoryService,IMapper mapper)
        {
            _courseCategoryService = courseCategoryService;

            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _courseCategoryService.GetAllAsync();

            var response = _mapper.Map<IEnumerable<CourseCategoryResponseDTO>>(categories);

            return Ok(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _courseCategoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Course category not found.");

            var response = _mapper.Map<CourseCategoryResponseDTO>(category);
            return Ok(response);
        }

        [HttpPost("AddCourseCategory")]
        public async Task<IActionResult> Create(CreateCourseCategoryDTO dto)
        {
            var category = await _courseCategoryService.CreateAsync(dto.Name, dto.Description);
            var response = _mapper.Map<CourseCategoryResponseDTO>(category);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCourseCategoryDTO dto)
        {
            var category = await _courseCategoryService.UpdateAsync(id, dto.Name, dto.Description);
            if (category == null)
                return NotFound("Course category not found.");

            var response = _mapper.Map<CourseCategoryResponseDTO>(category);
            return Ok(response);
        }

    }
}
