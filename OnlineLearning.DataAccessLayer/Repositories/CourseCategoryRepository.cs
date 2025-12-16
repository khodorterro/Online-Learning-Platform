using Microsoft.EntityFrameworkCore;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Repositories
{
    public class CourseCategoryRepository:ICourseCategoryRepository
    {
        private AppDbContext _appDbContext;
        public CourseCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<CourseCategory>> GetAllAsync()
        {
          return  await _appDbContext.CourseCategories.AsNoTracking().ToListAsync();
        }

        public async Task<CourseCategory?> GetByNameAsync(string name)
        {
            return await _appDbContext.CourseCategories
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<CourseCategory?> GetByIdAsync(int id)
        {
            return await _appDbContext.CourseCategories
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddAsync(CourseCategory courseCategory)
        {
            _appDbContext.CourseCategories.Add(courseCategory);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseCategory courseCategory)
        {
            _appDbContext.CourseCategories.Update(courseCategory);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
