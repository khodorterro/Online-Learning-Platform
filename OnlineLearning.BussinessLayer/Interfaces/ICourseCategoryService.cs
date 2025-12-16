using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public interface ICourseCategoryService
    {
        public Task<IEnumerable<CourseCategory>> GetAllAsync();
        public Task<CourseCategory> CreateAsync(string name ,string? description);
        public Task<CourseCategory?>UpdateAsync(int id,string name,string? description);
        public Task<CourseCategory?> GetByIdAsync(int id);

    }
}
