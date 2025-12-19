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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly ICourseRepository _courseRepository;

        public QuestionService(
            IQuestionRepository questionRepository,
            IQuizRepository quizRepository,
            ICourseRepository courseRepository)
        {
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
            _courseRepository = courseRepository;
        }

        private async Task ValidateInstructorOwnershipAsync(
            int quizId,
            int instructorId)
        {
            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            var course = await _courseRepository.GetByIdAsync(quiz.CourseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (course.CreatedBy != instructorId)
                throw new UnauthorizedAccessException(
                    "You are not allowed to manage questions for this course"
                );
        }

        public async Task<Question> GetByIdWithOwnershipAsync(
            int questionId,
            int instructorId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            await ValidateInstructorOwnershipAsync(
                question.QuizId,
                instructorId
            );

            return question;
        }

        public async Task<IEnumerable<Question>> GetByQuizIdWithOwnershipAsync(
            int quizId,
            int instructorId)
        {
            await ValidateInstructorOwnershipAsync(quizId, instructorId);

            return await _questionRepository.GetByQuizIdAsync(quizId);
        }

        public async Task<Question> CreateAsync(
            int quizId,
            string questionText,
            string questionType,
            int instructorId)
        {
            await ValidateInstructorOwnershipAsync(quizId, instructorId);

            var question = new Question
            {
                QuizId = quizId,
                QuestionText = questionText,
                QuestionType = questionType
            };

            await _questionRepository.AddAsync(question);
            return question;
        }

        public async Task<Question> UpdateAsync(
            int questionId,
            string questionText,
            string questionType,
            int instructorId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            await ValidateInstructorOwnershipAsync(
                question.QuizId,
                instructorId
            );

            question.QuestionText = questionText;
            question.QuestionType = questionType;

            await _questionRepository.UpdateAsync(question);
            return question;
        }

        public async Task DeleteAsync(
            int questionId,
            int instructorId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            await ValidateInstructorOwnershipAsync(
                question.QuizId,
                instructorId
            );

            await _questionRepository.DeleteAsync(question);
        }
    }
}
