using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public  class QuizService:IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ICourseRepository _courseRepo;
        private readonly IAccessValidatorService _accessValidatorService;
        public QuizService(IQuizRepository quizRepository, ICourseRepository courseRepo,IAccessValidatorService accessValidatorService)
        {
            _quizRepository = quizRepository;
            _courseRepo = courseRepo;
            _accessValidatorService = accessValidatorService;
        }
        private async Task ValidateInstructorOwnershipAsync(int courseId, int instructorId)
        {
            var course = await _courseRepo.GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (course.CreatedBy != instructorId)
                throw new UnauthorizedAccessException(
                    "You are not allowed to manage quizzes for this course"
                );
        }

        public async Task<Quiz> GetByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId,int userId,string role)
        {
            await _accessValidatorService.ValidateCourseAccessAsync(
                userId,
                role,
                courseId
            );

            return await _quizRepository.GetByCourseIdAsync(courseId);
        }


        public async Task<Quiz> CreateAsync(int courseId,int? lessonId,string title,
            int passingScore,int? timeLimit,int instructorId)
        {
            await ValidateInstructorOwnershipAsync(courseId, instructorId);

            var quiz = new Quiz
            {
                CourseId = courseId,
                LessonId = lessonId,
                Title = title,
                PassingScore = passingScore,
                TimeLimit = timeLimit
            };

            await _quizRepository.AddAsync(quiz);
            return quiz;
        }


        public async Task<Quiz?> UpdateAsync(int quizId,string title,int passingScore,int? timeLimit,int instructorId)
        {
            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
                return null;

            await ValidateInstructorOwnershipAsync(quiz.CourseId, instructorId);

            quiz.Title = title;
            quiz.PassingScore = passingScore;
            quiz.TimeLimit = timeLimit;

            await _quizRepository.UpdateAsync(quiz);
            return quiz;
        }


        public async Task DeleteAsync(int quizId, int instructorId)
        {
            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            await ValidateInstructorOwnershipAsync(quiz.CourseId, instructorId);

            await _quizRepository.DeleteAsync(quizId);
        }

    }
}

