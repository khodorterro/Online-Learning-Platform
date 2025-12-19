using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public  interface IQuestionRepository
    {
        Task<Question?> GetByIdAsync(int id);

        Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId);

        Task<int> CountByQuizIdAsync(int quizId);

        Task<IEnumerable<Question>> GetRandomByQuizIdAsync(
            int quizId,
            int count
        );

        Task AddAsync(Question question);

        Task UpdateAsync(Question question);

        Task DeleteAsync(Question question);
    }
}
