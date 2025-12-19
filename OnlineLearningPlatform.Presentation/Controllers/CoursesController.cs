using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/courses")]
    [ApiController]
    [Authorize] 
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService,IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var courses = await _courseService.GetAvailableCoursesAsync(userId, role);

            return Ok(_mapper.Map<IEnumerable<CourseResponseDTO>>(courses));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = User.GetUserId();
            string role = User.GetRole();

            var course = await _courseService.GetByIdWithAccessAsync(id, userId, role);

            return Ok(_mapper.Map<CourseResponseDTO>(course));
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO dto)
        {
            int instructorId = User.GetUserId();

            var course = await _courseService.CreateAsync(dto.Title,dto.ShortDescription,dto.LongDescription,
                dto.CategoryId,dto.Difficulty,dto.Thumbnail,instructorId);

            return CreatedAtAction(nameof(GetById),new { id = course.Id },_mapper.Map<CourseResponseDTO>(course));
        }

        [Authorize(Roles = "Instructor")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCourseDTO dto)
        {
            int instructorId = User.GetUserId();

            var course = await _courseService.UpdateAsync(id,dto.Title,dto.ShortDescription,dto.LongDescription,
                dto.Difficulty,dto.Thumbnail,dto.IsPublished,instructorId);

            return Ok(_mapper.Map<CourseResponseDTO>(course));
        }
    }
}
