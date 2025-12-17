using AutoMapper;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearningPlatform.Presentation.DTOs;
using OnlineLearningPlatform.Presentation.DTOs.AnswerDTOs;
using OnlineLearningPlatform.Presentation.DTOs.CourseCategoryDTOs;
using OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs;
using OnlineLearningPlatform.Presentation.DTOs.EnrolledCourseDTOs;
using OnlineLearningPlatform.Presentation.DTOs.LessonDTOs;
using OnlineLearningPlatform.Presentation.DTOs.QuestionDTOs;
using OnlineLearningPlatform.Presentation.DTOs.QuizAttemptAnswerDTOs;
using OnlineLearningPlatform.Presentation.DTOs.QuizAttemptDTOs;
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

            CreateMap<QuizAttempt, QuizAttemptResponseDTO>();

            CreateMap<QuizAttemptAnswer, QuizAttemptAnswerResponseDTO>();

            CreateMap<QuizAttemptAnswer, AttemptQuestionDTO>()
                .ForMember(dest => dest.QuestionId, opt => opt
                      .MapFrom(src => src.Question != null ? src.Question.Id : 0))
                .ForMember(dest => dest.QuestionText, opt => opt
                       .MapFrom(src => src.Question != null ? src.Question.QuestionText : string.Empty))
                .ForMember(dest => dest.QuestionType, opt => opt
                       .MapFrom(src => src.Question != null ? src.Question.QuestionType : string.Empty))
                .ForMember(dest => dest.Answers, opt => opt
                       .MapFrom(src => src.Question != null ? src.Question.Answers : new List<Answer>()));

            CreateMap<Answer, AttemptAnswerDTO>();

            CreateMap<EnrolledCourse, EnrolledCourseResponseDTO>()
              .ForMember(dest => dest.CourseTitle,
                     opt => opt.MapFrom(src => src.Course.Title));



        }
    }
}
