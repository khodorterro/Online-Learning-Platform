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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IQuizRepository _quizRepo;
        private readonly ICourseRepository _courseRepo;

        public AnswerService(
            IAnswerRepository answerRepo,
            IQuestionRepository questionRepo,
            IQuizRepository quizRepo,
            ICourseRepository courseRepo)
        {
            _answerRepo = answerRepo;
            _questionRepo = questionRepo;
            _quizRepo = quizRepo;
            _courseRepo = courseRepo;
        }

        public async Task<Answer> GetByIdAsync(int id)
        {
            var answer = await _answerRepo.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException("Answer not found");

            return answer;
        }

        public async Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId)
        {
            return await _answerRepo.GetByQuestionIdAsync(questionId);
        }

        public async Task<Answer> CreateAsync(
            int questionId,
            string answerText,
            bool isCorrect,
            int instructorId)
        {
            await ValidateInstructorOwnershipByQuestionAsync(
                questionId,
                instructorId
            );

            var answer = new Answer
            {
                QuestionId = questionId,
                AnswerText = answerText,
                IsCorrect = isCorrect
            };

            await _answerRepo.AddAsync(answer);
            return answer;
        }

        public async Task<Answer> UpdateAsync(
            int id,
            string answerText,
            bool isCorrect,
            int instructorId)
        {
            var answer = await _answerRepo.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException("Answer not found");

            await ValidateInstructorOwnershipByQuestionAsync(
                answer.QuestionId,
                instructorId
            );

            answer.AnswerText = answerText;
            answer.IsCorrect = isCorrect;

            await _answerRepo.UpdateAsync(answer);
            return answer;
        }

        public async Task DeleteAsync(int id, int instructorId)
        {
            var answer = await _answerRepo.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException("Answer not found");

            await ValidateInstructorOwnershipByQuestionAsync(
                answer.QuestionId,
                instructorId
            );

            await _answerRepo.DeleteAsync(id);
        }

        private async Task ValidateInstructorOwnershipByQuestionAsync(
            int questionId,
            int instructorId)
        {
            var question = await _questionRepo.GetByIdAsync(questionId);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            var quiz = await _quizRepo.GetByIdAsync(question.QuizId);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            var course = await _courseRepo.GetByIdAsync(quiz.CourseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (course.CreatedBy != instructorId)
                throw new UnauthorizedAccessException(
                    "You are not allowed to modify answers for this course"
                );
        }
    }
}
