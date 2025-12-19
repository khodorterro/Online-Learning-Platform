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
        private readonly IEnrolledCourseRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrolledCourseService(
            IEnrolledCourseRepository enrollmentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
        }

        public async Task EnrollAsync(int userId, int courseId)
        {

            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            bool alreadyEnrolled =
                await _enrollmentRepository.IsUserEnrolledAsync(userId, courseId);

            if (alreadyEnrolled)
                throw new InvalidOperationException(
                    "User is already enrolled in this course"
                );

            var enrollment = new EnrolledCourse
            {
                UserId = userId,
                CourseId = courseId,
                EnrolledDate = DateTime.UtcNow
            };

            await _enrollmentRepository.AddAsync(enrollment);
        }

        public async Task<IEnumerable<EnrolledCourse>> GetUserEnrollmentsAsync(int userId)
        {
            return await _enrollmentRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Course>> GetMyEnrolledCoursesAsync(int userId)
        {
            var courseIds = await _enrollmentRepository
                .GetCourseIdsByUserAsync(userId);

            if (!courseIds.Any())
                return Enumerable.Empty<Course>();

            return await _courseRepository.GetByIdsAsync(courseIds);
        }

        public async Task<bool> IsUserEnrolledAsync(int userId, int courseId)
        {
            return await _enrollmentRepository.IsUserEnrolledAsync(userId, courseId);
        }
    }
}
