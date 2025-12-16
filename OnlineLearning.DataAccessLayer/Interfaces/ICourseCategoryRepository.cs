using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public interface ICourseCategoryRepository
    {
        public  Task<CourseCategory?> GetByIdAsync(int id);
        public  Task<IEnumerable<CourseCategory>> GetAllAsync();
        public Task<CourseCategory?> GetByNameAsync(string name);
        public Task AddAsync(CourseCategory course);
        public Task UpdateAsync(CourseCategory course);

    }
}
