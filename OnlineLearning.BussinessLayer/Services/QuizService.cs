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
        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public async Task<Quiz> GetByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId)
        {
            return await _quizRepository.GetByCourseIdAsync(courseId);
        }

        public async Task<Quiz> CreateAsync(int courseId,int? lessonId,string title, int passingScore, int? timeLimit)
        {
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

        public async Task<Quiz> UpdateAsync(int id, string title,int passingScore,int? timeLimit)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            quiz.Title = title;
            quiz.PassingScore = passingScore;
            quiz.TimeLimit = timeLimit;

            await _quizRepository.UpdateAsync(quiz);
            return quiz;
        }

        public async Task DeleteAsync(int id)
        {
            await _quizRepository.DeleteAsync(id);
        }
    }
}

