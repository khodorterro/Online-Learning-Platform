using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public interface ICourseRepository
    {
        public Task<Course?> GetByIdAsync(int id);
        public Task<IEnumerable<Course>> GetAllAsync();
        public Task<IEnumerable<Course>> GetPublichedAsync();
        public Task DeleteAsync(int id);
        public Task UpdateAsync(Course course);
        public Task AddAsync(Course course);
        public Task<IEnumerable<Course>> GetByCategoryId(int categoryId);
        Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<int> courseIds);

    }
}
