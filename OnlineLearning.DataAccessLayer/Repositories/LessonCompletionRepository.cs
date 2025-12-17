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
    public  class LessonCompletionRepository: ILessonCompletionRepository
    {
        private readonly AppDbContext _appDbContext;
        public LessonCompletionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> IsCompletedAsync(int userId, int lessonId)
        {
            return await _appDbContext.LessonCompletions.AnyAsync(cl=>cl.UserId==userId &&  cl.LessonId==lessonId);
        }
        public async Task AddAsync(LessonCompletion lessonCompletion)
        {
            _appDbContext.LessonCompletions.Add(lessonCompletion);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable< LessonCompletion>>GetByUserIdAsync(int userid)
        {
            return await _appDbContext.LessonCompletions
                .Include(lc=>lc.Lesson).Where(lc=>lc.UserId==userid).ToListAsync();
        }
        public async Task<int> CountCompletedLessonsAsync(int userId, int courseId)
        {
            return await _appDbContext.LessonCompletions
                .CountAsync(lc => lc.UserId == userId && lc.Lesson.CourseId == courseId);
        }
    }
}
