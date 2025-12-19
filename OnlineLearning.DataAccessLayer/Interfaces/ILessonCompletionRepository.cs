using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public  interface ILessonCompletionRepository
    {
        Task<bool> IsLessonCompletedAsync(int userId, int lessonId);
        Task AddAsync(LessonCompletion completion);

        Task<IEnumerable<LessonCompletion>> GetByUserIdAsync(int userId);
        Task<int> CountCompletedLessonsAsync(int userId, int courseId);
    }
}
