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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _appDbContext;

        public AnswerRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<Answer?> GetByIdAsync(int id)
        {
            return await _appDbContext.Answers
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId)
        {
            return await _appDbContext.Answers
                .AsNoTracking()
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }

        public async Task AddAsync(Answer answer)
        {
            await _appDbContext.Answers.AddAsync(answer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Answer answer)
        {
            _appDbContext.Answers.Update(answer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var answer = await _appDbContext.Answers.FindAsync(id);
            if (answer == null)
                throw new KeyNotFoundException("Answer not found");

            _appDbContext.Answers.Remove(answer);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
