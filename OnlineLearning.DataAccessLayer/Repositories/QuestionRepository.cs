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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _appDbContext;

        public QuestionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await _appDbContext.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId)
        {
            return await _appDbContext.Questions
                .AsNoTracking()
                .Where(q => q.QuizId == quizId)
                .ToListAsync();
        }

        public async Task AddAsync(Question question)
        {
            await _appDbContext.Questions.AddAsync(question);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question question)
        {
            _appDbContext.Questions.Update(question);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _appDbContext.Questions.FindAsync(id);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            _appDbContext.Questions.Remove(question);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<List<Question>> GetRandomByQuizIdAsync(int quizId, int count)
        {
            return await _appDbContext.Questions
                .Where(q => q.QuizId == quizId)
                .OrderBy(q => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }
        public async Task<int> CountByQuizIdAsync(int quizId)
        {
            return await _appDbContext.Questions.Where(q=>q.QuizId==quizId).CountAsync();
        }
    }
}
