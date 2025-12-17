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
            // 1️⃣ Ensure user is enrolled
            if (!await _enrollmentRepo.IsEnrolledAsync(userId, courseId))
                throw new InvalidOperationException("User is not enrolled in this course");

            // 2️⃣ Count total lessons
            int totalLessons = await _lessonRepo.CountByCourseIdAsync(courseId);
            if (totalLessons == 0)
                return 0;

            // 3️⃣ Count completed lessons
            int completedLessons =
                await _completionRepo.CountCompletedLessonsAsync(userId, courseId);

            // 4️⃣ Calculate percentage
            return (int)Math.Round((double)completedLessons / totalLessons * 100);
        }
    }
}
