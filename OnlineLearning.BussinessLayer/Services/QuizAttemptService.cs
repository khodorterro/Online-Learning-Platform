using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class QuizAttemptService :IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizAttemptAnswerRepository _quizAttemptAnswerRepository;
        private readonly IAccessValidatorService _accessValidator;

        public QuizAttemptService(
            IQuizAttemptRepository quizAttemptRepository,
            IQuizRepository quizRepository,
            IQuestionRepository questionRepository,
            IQuizAttemptAnswerRepository quizAttemptAnswerRepository,
            IAccessValidatorService accessValidator)
        {
            _quizAttemptRepository = quizAttemptRepository;
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
            _quizAttemptAnswerRepository = quizAttemptAnswerRepository;
            _accessValidator = accessValidator;
        }


        public async Task<QuizAttempt> GetByIdAsync(int id)
        {
            var attempt = await _quizAttemptRepository.GetByIdAsync(id);
            if (attempt == null)
                throw new KeyNotFoundException("Quiz attempt not found");

            return attempt;
        }

        public async Task<IEnumerable<QuizAttempt>> GetByUserIdAsync(int userId)
        {
            return await _quizAttemptRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId)
        {
            return await _quizAttemptRepository.GetByQuizIdAsync(quizId);
        }


        public async Task<bool> IsPassedAsync(int attemptId)
        {
            var attempt = await _quizAttemptRepository.GetByIdAsync(attemptId);
            if (attempt == null)
                throw new KeyNotFoundException("Attempt not found");

            int passingScore = await _quizRepository.GetPassingScoreAsync(attempt.QuizId);
            return attempt.Score >= passingScore;
        }


        public async Task<QuizAttempt> CreateAsync(int quizId, int userId, string role)
        {

            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            await _accessValidator.ValidateCourseAccessAsync(
                userId,
                role,
                quiz.CourseId
            );

            int totalQuestions = await _questionRepository.CountByQuizIdAsync(quizId);
            if (totalQuestions < 4)
                throw new InvalidOperationException(
                    "Quiz must contain at least 4 questions."
                );

            var attempt = new QuizAttempt
            {
                QuizId = quizId,
                UserId = userId,
                AttemptDate = DateTime.UtcNow,
                IsSubmitted = false,
                Score = 0
            };

            await _quizAttemptRepository.AddAsync(attempt);


            var selectedQuestions = await _questionRepository
                .GetRandomByQuizIdAsync(quizId, 4);

            
            var attemptAnswers = selectedQuestions.Select(q => new QuizAttemptAnswer
            {
                AttemptId = attempt.Id,
                QuestionId = q.Id,
                SelectedAnswerId = null,
                IsCorrect = false,
                AnsweredAt = DateTime.UtcNow
            }).ToList();

            await _quizAttemptAnswerRepository.AddRangeAsync(attemptAnswers);

            return attempt;
        }
    }
}
