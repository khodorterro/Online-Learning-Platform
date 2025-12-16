using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IQuizService
    {
        Task<Quiz> GetByIdAsync(int id);
        Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId);

        Task<Quiz> CreateAsync(int courseId,int? lessonId, string title,int passingScore,int? timeLimit);

        Task<Quiz> UpdateAsync( int id,string title, int passingScore,int? timeLimit );

        Task DeleteAsync(int id);
    }
}

