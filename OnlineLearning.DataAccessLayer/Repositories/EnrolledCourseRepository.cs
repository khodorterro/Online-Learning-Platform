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
    public class EnrolledCourseRepository:IEnrolledCourseRepository
    {
        private readonly AppDbContext _appDbContext;

        public EnrolledCourseRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<bool> IsEnrolledAsync(int userId, int courseId)
        {
            return await _appDbContext.EnrolledCourses.AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        public async Task AddAsync(EnrolledCourse enrolledCourse)
        {
            _appDbContext.EnrolledCourses.Add(enrolledCourse);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnrolledCourse>> GetByUserIdAsync(int userId)
        {
            return await _appDbContext.EnrolledCourses
                .Include(e => e.Course)
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }
    }
}
