using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public interface IQuizAttemptRepository
    {
        Task<QuizAttempt?> GetByIdAsync(int id);
        Task<IEnumerable<QuizAttempt>> GetByUserIdAsync(int userId);
        Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId);
        Task MarkAsSubmittedAsync(int attemptId);
        Task UpdateScoreAsync(int attemptId, int score);

        Task AddAsync(QuizAttempt attempt);
    }
}
