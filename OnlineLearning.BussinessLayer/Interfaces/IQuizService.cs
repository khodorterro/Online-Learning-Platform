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
        Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId,int userId,string role);

        Task<Quiz> CreateAsync(int courseId,int? lessonId, string title,int passingScore,int? timeLimit,int instructorId);

        Task<Quiz?> UpdateAsync( int id,string title, int passingScore,int? timeLimit, int instructorId);

        Task DeleteAsync(int id, int instructorId);
    }
}

