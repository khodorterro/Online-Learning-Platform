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
    public class QuizRepository:IQuizRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuizRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _appDbContext.Quizzes
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId)
        {
            return await _appDbContext.Quizzes
                .AsNoTracking()
                .Where(q => q.CourseId == courseId)
                .ToListAsync();
        }

        public async Task AddAsync(Quiz quiz)
        {
            await _appDbContext.Quizzes.AddAsync(quiz);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Quiz quiz)
        {
            _appDbContext.Quizzes.Update(quiz);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quiz = await _appDbContext.Quizzes.FindAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException("Quiz not found");

            _appDbContext.Quizzes.Remove(quiz);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
