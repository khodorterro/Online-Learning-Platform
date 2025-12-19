using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IQuizAttemptService
    {
        Task<QuizAttempt> GetByIdAsync(int id);
        Task<IEnumerable<QuizAttempt>> GetByUserIdAsync(int userId);
        Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId);

        Task<QuizAttempt> CreateAsync(int quizId, int userId, string role);
        Task<bool> IsPassedAsync(int attemptId);
    }
}
