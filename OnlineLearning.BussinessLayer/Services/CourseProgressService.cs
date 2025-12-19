using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public  class CourseProgressService:ICourseProgressService
    {
        private readonly ILessonRepository _lessonRepo;
        private readonly ILessonCompletionRepository _completionRepo;
        private readonly IEnrolledCourseRepository _enrollmentRepo;

        public CourseProgressService(
            ILessonRepository lessonRepo,
            ILessonCompletionRepository completionRepo,
            IEnrolledCourseRepository enrollmentRepo)
        {
            _lessonRepo = lessonRepo;
            _completionRepo = completionRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public async Task<int> GetCourseProgressAsync(int userId, int courseId)
        {
            if (!await _enrollmentRepo.IsUserEnrolledAsync(userId, courseId))
                throw new UnauthorizedAccessException(
                    "You are not enrolled in this course"
                );


            int totalLessons = await _lessonRepo.CountByCourseIdAsync(courseId);
            if (totalLessons == 0)
                return 0;


            int completedLessons =
                await _completionRepo.CountCompletedLessonsAsync(userId, courseId);

            int percentage = (int)Math.Round(
                (double)completedLessons / totalLessons * 100
            );

            return Math.Min(percentage, 100);
        }

        public async Task<int> GetStudentProgressAsync(int courseId,int studentId,int requesterId,string role)
        {
            if (role == "Admin")
            {
                return await GetCourseProgressAsync(studentId, courseId);
            }

            if (role == "Instructor")
            {
                bool ownsCourse =
                    await _enrollmentRepo.IsInstructorOwnerOfCourseAsync(requesterId,courseId);

                if (!ownsCourse)
                    throw new UnauthorizedAccessException(
                        "You do not own this course"
                    );

                return await GetCourseProgressAsync(studentId, courseId);
            }

            throw new UnauthorizedAccessException("Access denied");
        }

    }
}
