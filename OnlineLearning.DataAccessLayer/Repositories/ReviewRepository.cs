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
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int courseId)
        {
            return await _context.Reviews
                .AnyAsync(r =>
                    r.UserId == userId &&
                    r.CourseId == courseId);
        }


        public async Task<IEnumerable<Review>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Reviews
                .Where(r => r.CourseId == courseId)
                .Include(r => r.User) 
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
    }
}
