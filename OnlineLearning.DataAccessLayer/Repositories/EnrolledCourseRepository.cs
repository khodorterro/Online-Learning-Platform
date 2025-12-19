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
        private readonly AppDbContext _context;

        public EnrolledCourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserEnrolledAsync(int userId, int courseId)
        {
            return await _context.EnrolledCourses
                .AnyAsync(ec =>
                    ec.UserId == userId &&
                    ec.CourseId == courseId);
        }

        public async Task<List<int>> GetCourseIdsByUserAsync(int userId)
        {
            return await _context.EnrolledCourses
                .Where(ec => ec.UserId == userId)
                .Select(ec => ec.CourseId)
                .ToListAsync();
        }

        public async Task AddAsync(EnrolledCourse enrollment)
        {
            await _context.EnrolledCourses.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsInstructorOwnerOfCourseAsync(int instructorId,int courseId)
        {
            return await _context.Courses
                .AnyAsync(c => c.Id == courseId && c.CreatedBy == instructorId);
        }
        public async Task<IEnumerable<EnrolledCourse>> GetByUserIdAsync(int userId)
        {
            return await _context.EnrolledCourses
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .OrderByDescending(e => e.EnrolledDate)
                .ToListAsync();
        }

    }
}
