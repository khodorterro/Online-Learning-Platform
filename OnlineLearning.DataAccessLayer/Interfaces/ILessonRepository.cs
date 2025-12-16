using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public interface  ILessonRepository
    {
        public Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId);
        public Task<Lesson?> GetByIdAsync(int LessonID);
        public Task AddAsync(Lesson lesson);
        public Task UpdateAsync(Lesson lesson);
        public Task DeleteAsync(int LessonID);

    }
}
