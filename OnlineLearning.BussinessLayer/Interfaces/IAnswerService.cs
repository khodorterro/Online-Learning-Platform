using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public interface IAnswerService{

        Task<Answer> GetByIdAsync(int id);
        Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId);
        Task<Answer> CreateAsync(int questionId, string answerText, bool isCorrect, int instructorId);
        Task<Answer> UpdateAsync(int id, string answerText, bool isCorrect, int instructorId);
        Task DeleteAsync(int id, int instructorId);
    }
}
