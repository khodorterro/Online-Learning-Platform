using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class EnrolledCourseService:IEnrolledCourseService
    {
        private readonly IEnrolledCourseRepository _repo;

        public EnrolledCourseService(IEnrolledCourseRepository repo)
        {
            _repo = repo;
        }

        public async Task EnrollAsync(int userId, int courseId)
        {
            if (await _repo.IsEnrolledAsync(userId, courseId))
                throw new InvalidOperationException("User already enrolled in this course");

            var enrollment = new EnrolledCourse
            {
                UserId = userId,
                CourseId = courseId,
                EnrolledDate = DateTime.UtcNow
            };

            await _repo.AddAsync(enrollment);
        }

        public async Task<IEnumerable<EnrolledCourse>> GetUserEnrollmentsAsync(int userId)
        {
            return await _repo.GetByUserIdAsync(userId);
        }
    }
}
