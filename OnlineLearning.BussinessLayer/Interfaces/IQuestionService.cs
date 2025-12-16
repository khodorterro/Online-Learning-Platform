using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IQuestionService
    {
        Task<Question> GetByIdAsync(int id);
        Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId);

        Task<Question> CreateAsync(int quizId,string questionText,string questionType);

        Task<Question> UpdateAsync(int id,string questionText,  string questionType );

        Task DeleteAsync(int id);
    }
}

