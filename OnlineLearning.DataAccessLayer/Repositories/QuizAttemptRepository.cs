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
    public class QuizAttemptRepository:IQuizAttemptRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuizAttemptRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<QuizAttempt?> GetByIdAsync(int id)
        {
            return await _appDbContext.QuizAttempts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<QuizAttempt>> GetByUserIdAsync(int userId)
        {
            return await _appDbContext.QuizAttempts
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.AttemptDate)
                .ToListAsync();
        }
        public async Task UpdateScoreAsync(int attemptId, int score)
        {
            var attempt = await _appDbContext.QuizAttempts.FindAsync(attemptId);
            if (attempt == null)
                throw new KeyNotFoundException("Quiz attempt not found");

            attempt.Score = score;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task MarkAsSubmittedAsync(int attemptId)
        {
            var attempt = await _appDbContext.QuizAttempts.FindAsync(attemptId);
            if (attempt == null)
                throw new KeyNotFoundException("Quiz attempt not found");

            attempt.IsSubmitted = true;
            await _appDbContext.SaveChangesAsync();
        }



        public async Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId)
        {
            return await _appDbContext.QuizAttempts
                .AsNoTracking()
                .Where(a => a.QuizId == quizId)
                .OrderByDescending(a => a.AttemptDate)
                .ToListAsync();
        }

        public async Task AddAsync(QuizAttempt attempt)
        {
            await _appDbContext.QuizAttempts.AddAsync(attempt);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

