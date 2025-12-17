using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public  interface IQuizAttemptAnswerRepository
    {
        Task<IEnumerable<QuizAttemptAnswer>> GetByAttemptIdAsync(int attemptId);
        Task AddAsync(QuizAttemptAnswer answer);
        Task AddRangeAsync(IEnumerable<QuizAttemptAnswer> answers);
        Task UpdateRangeAsync(IEnumerable<QuizAttemptAnswer> answers);
        Task<IEnumerable<QuizAttemptAnswer>> GetWithQuestionAndAnswersAsync(int attemptId);

    }
}
