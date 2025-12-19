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
        Task<Question> GetByIdWithOwnershipAsync(int questionId,int instructorId);

        Task<IEnumerable<Question>> GetByQuizIdWithOwnershipAsync(int quizId,int instructorId);

        Task<Question> CreateAsync(int quizId,string questionText,string questionType,int instructorId);

        Task<Question> UpdateAsync(int questionId,string questionText,string questionType,int instructorId);

        Task DeleteAsync(int questionId,int instructorId);
    }
}


