using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IQuizAttemptAnswerService
    {
        Task<IEnumerable<QuizAttemptAnswer>> GetByAttemptIdAsync(int attemptId);

        Task SubmitAnswersAsync(int attemptId, IEnumerable<(int QuestionId, int SelectedAnswerId)> answers);

        Task<IEnumerable<QuizAttemptAnswer>> GetQuestionsByAttemptAsync(int attemptId);

    }
}
