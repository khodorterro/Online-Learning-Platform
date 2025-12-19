using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class AccessValidatorService : IAccessValidatorService
    {
        private readonly IEnrolledCourseRepository _enrollmentRepo;
        private readonly ICourseRepository _courseRepo;

        public AccessValidatorService(
            IEnrolledCourseRepository enrollmentRepo,
            ICourseRepository courseRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _courseRepo = courseRepo;
        }

        public async Task ValidateCourseAccessAsync(int userId,string role,int courseId)
        {
            if (role == "Admin")
                return;

            var course = await _courseRepo.GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (role == "Instructor")
            {
                if (course.CreatedBy != userId)
                    throw new UnauthorizedAccessException(
                        "You do not own this course"
                    );
                return;
            }

            if (role == "Student")
            {
                bool enrolled = await _enrollmentRepo
                    .IsUserEnrolledAsync(userId, courseId);

                if (!enrolled)
                    throw new UnauthorizedAccessException(
                        "You must be enrolled in this course"
                    );
            }
        }
    }

}
