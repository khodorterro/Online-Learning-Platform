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
        private readonly AppDbContext _context;

        public LessonCompletionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsLessonCompletedAsync(int userId, int lessonId)
        {
            return await _context.LessonCompletions
                .AnyAsync(lc =>
                    lc.UserId == userId &&
                    lc.LessonId == lessonId);
        }

        public async Task AddAsync(LessonCompletion completion)
        {
            await _context.LessonCompletions.AddAsync(completion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LessonCompletion>> GetByUserIdAsync(int userId)
        {
            return await _context.LessonCompletions
                .Include(lc => lc.Lesson)
                .Where(lc => lc.UserId == userId)
                .OrderByDescending(lc => lc.CompletedDate)
                .ToListAsync();
        }
        public async Task<int> CountCompletedLessonsAsync(int userId, int courseId)
        {
            return await _context.LessonCompletions
                .CountAsync(lc =>
                    lc.UserId == userId &&
                    lc.Lesson.CourseId == courseId
                );
        }
    }
}
