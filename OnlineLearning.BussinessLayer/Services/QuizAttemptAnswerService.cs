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
    public class QuizAttemptAnswerService : IQuizAttemptAnswerService
    {
        private readonly IQuizAttemptAnswerRepository _attemptAnswerRepo;
        private readonly IAnswerRepository _answerRepo;
        private readonly IQuizAttemptRepository _attemptRepo;

        public QuizAttemptAnswerService(
            IQuizAttemptAnswerRepository attemptAnswerRepo,
            IAnswerRepository answerRepo,
            IQuizAttemptRepository attemptRepo)
        {
            _attemptAnswerRepo = attemptAnswerRepo;
            _answerRepo = answerRepo;
            _attemptRepo = attemptRepo;
        }


        public async Task<IEnumerable<QuizAttemptAnswer>> GetQuestionsByAttemptAsync(int attemptId)
        {
            var attempt = await _attemptRepo.GetByIdAsync(attemptId);
            if (attempt == null)
                throw new KeyNotFoundException("Quiz attempt not found");

            return await _attemptAnswerRepo.GetWithQuestionAndAnswersAsync(attemptId);
        }



        public async Task<IEnumerable<QuizAttemptAnswer>> GetByAttemptIdAsync(int attemptId)
        {
            return await _attemptAnswerRepo.GetByAttemptIdAsync(attemptId);
        }

        public async Task SubmitAnswersAsync(int attemptId,IEnumerable<(int QuestionId, int SelectedAnswerId)> answers)
        {
            var attempt = await _attemptRepo.GetByIdAsync(attemptId);
            if (attempt == null)
                throw new KeyNotFoundException("Quiz attempt not found");

            if (attempt.IsSubmitted)
                throw new InvalidOperationException("This quiz attempt is already submitted");

            //  Get locked questions 
            var lockedAnswers = (await _attemptAnswerRepo.GetByAttemptIdAsync(attemptId))
                .ToList();

            int score = 0;

            foreach (var answer in answers)
            {
                var attemptAnswer = lockedAnswers
                    .FirstOrDefault(a => a.QuestionId == answer.QuestionId);

                //  Ensure question belongs to this attempt
                if (attemptAnswer == null)
                    throw new InvalidOperationException(
                        "This question does not belong to this quiz attempt."
                    );

                //  Prevent double answering
                if (attemptAnswer.SelectedAnswerId != null)
                    throw new InvalidOperationException(
                        "This question has already been answered."
                    );

                bool isCorrect = await _answerRepo
                    .IsCorrectAnswerAsync(answer.SelectedAnswerId);

                if (isCorrect)
                    score=score+(100/lockedAnswers.Count());

                //  UPDATE existing row 
                attemptAnswer.SelectedAnswerId = answer.SelectedAnswerId;
                attemptAnswer.IsCorrect = isCorrect;
                attemptAnswer.AnsweredAt = DateTime.UtcNow;
            }

            // Save updates
            await _attemptAnswerRepo.UpdateRangeAsync(lockedAnswers);

            // Update score & lock attempt
            await _attemptRepo.UpdateScoreAsync(attemptId, score);
            await _attemptRepo.MarkAsSubmittedAsync(attemptId);
        }


    }
}
