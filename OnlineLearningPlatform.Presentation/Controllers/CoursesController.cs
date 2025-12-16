using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CoursesController(ICourseService courseService,IMapper mapper)
        {
            _courseService = courseService;
            _mapper= mapper;
        }

        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<CourseResponseDTO>>(courses);
            return Ok(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound("Course not found.");

            var response = _mapper.Map<CourseResponseDTO>(course);
            return Ok(response);
        }


        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO dto)
        {
            var course = await _courseService.CreateAsync(
                dto.Title,
                dto.ShortDescription,
                dto.LongDescription,
                dto.CategoryId,
                dto.Difficulty,
                dto.Thumbnail,
                dto.CreatedBy
            );

            var response = _mapper.Map<CourseResponseDTO>(course);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCourseDTO dto)
        {
            var course = await _courseService.UpdateAsync(
                id,
                dto.Title,
                dto.ShortDescription,
                dto.LongDescription,
                dto.Difficulty,
                dto.Thumbnail,
                dto.IsPublished
            );

            if (course == null)
                return NotFound("Course not found.");

            var response = _mapper.Map<CourseResponseDTO>(course);
            return Ok(response);
        }

    }
}
