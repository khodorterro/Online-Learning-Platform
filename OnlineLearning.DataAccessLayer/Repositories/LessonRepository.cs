using Microsoft.EntityFrameworkCore;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Repositories
{
    public  class LessonRepository : ILessonRepository
    {
        private AppDbContext _appDbContext;
        public LessonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Lesson?> GetByIdAsync(int id)
        {
            return await _appDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int id)
        {
            return await _appDbContext.Lessons.AsNoTracking().Where(l => l.CourseId == id).OrderBy(l=>l.Order).ToListAsync();
        }
        public async Task AddAsync(Lesson lesson)
        {
            await _appDbContext.Lessons.AddAsync(lesson);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Lesson lesson)
        {
             _appDbContext.Lessons.Update(lesson);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson=_appDbContext.Lessons.FirstOrDefault(l => l.Id == id);

            if (lesson == null)
                return;
            _appDbContext.Lessons.Remove(lesson);
            await _appDbContext.SaveChangesAsync();

        }
    }
}
