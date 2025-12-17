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
    public class QuizAttemptAnswerRepository:IQuizAttemptAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuizAttemptAnswerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<QuizAttemptAnswer>> GetByAttemptIdAsync(int attemptId)
        {
            return await _appDbContext.QuizAttemptAnswers
                .AsNoTracking()
                .Where(a => a.AttemptId == attemptId)
                .ToListAsync();
        }

        public async Task AddAsync(QuizAttemptAnswer answer)
        {
            await _appDbContext.QuizAttemptAnswers.AddAsync(answer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<QuizAttemptAnswer> answers)
        {
            await _appDbContext.QuizAttemptAnswers.AddRangeAsync(answers);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<QuizAttemptAnswer> answers)
        {
            _appDbContext.QuizAttemptAnswers.UpdateRange(answers);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuizAttemptAnswer>> GetWithQuestionAndAnswersAsync(int attemptId)
        {
            return await _appDbContext.QuizAttemptAnswers
                .Include(a => a.Question)
                .ThenInclude(q => q.Answers)
                .Where(a => a.AttemptId == attemptId)
                .AsNoTracking()
                .ToListAsync();
        }


    }
}
