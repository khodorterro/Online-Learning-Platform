using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface ILessonCompletionService
    {
        Task CompleteLessonAsync(int userId, int lessonId);
        Task<IEnumerable<LessonCompletion>> GetCompletedLessonsAsync(int userId);
    }
}
