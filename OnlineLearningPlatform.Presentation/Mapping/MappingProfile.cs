using AutoMapper;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearningPlatform.Presentation.DTOs;
using OnlineLearningPlatform.Presentation.DTOs.CourseCategoryDTOs;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;
using OnlineLearningPlatform.Presentation.DTOs.LessonDTOs;

namespace OnlineLearningPlatform.Presentation.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDTO>();

            CreateMap<Course, CourseResponseDTO>();

            CreateMap<CourseCategory, CourseCategoryResponseDTO>();

            CreateMap<Lesson,LessonResponseDTO>();
        }
    }
}
