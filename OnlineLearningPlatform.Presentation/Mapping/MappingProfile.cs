using AutoMapper;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearningPlatform.Presentation.DTOs;
using OnlineLearningPlatform.Presentation.DTOs.AnswerDTOs;
using OnlineLearningPlatform.Presentation.DTOs.CourseCategoryDTOs;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;
using OnlineLearningPlatform.Presentation.DTOs.LessonDTOs;
using OnlineLearningPlatform.Presentation.DTOs.QuestionDTOs;
using OnlineLearningPlatform.Presentation.DTOs.QuizDTOs;

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

            CreateMap<Quiz, QuizResponseDTO>();

            CreateMap<Question, QuestionResponseDTO>();

            CreateMap<Answer, AnswerResponseDTO>();
        }
    }
}
