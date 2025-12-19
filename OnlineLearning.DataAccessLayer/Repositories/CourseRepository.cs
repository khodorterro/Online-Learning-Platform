using Microsoft.EntityFrameworkCore;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext appDbContext;
        public CourseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
           return await  appDbContext.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        }
        public async Task AddAsync(Course course)
        {
            await appDbContext.Courses.AddAsync(course);
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            appDbContext.Courses.Update(course);
            await appDbContext.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var course = await appDbContext.Courses.FindAsync(id);
            if (course ==null)
                return;
            appDbContext.Courses.Remove(course);
            await appDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Course>> GetPublichedAsync()
        {
            return await appDbContext.Courses.AsNoTracking().Where(c=>c.IsPublished).ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await appDbContext.Courses.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetByCategoryId(int categoryId)
        {
            return await appDbContext.Courses.AsNoTracking().Where(c=>c.CategoryId == categoryId).ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<int> courseIds)
        {
            return await appDbContext.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();
        }

    }
}
