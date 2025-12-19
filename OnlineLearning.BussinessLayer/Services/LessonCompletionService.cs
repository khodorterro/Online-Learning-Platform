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
    public  class LessonCompletionService:ILessonCompletionService
    {
        private readonly ILessonCompletionRepository _completionRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IEnrolledCourseRepository _enrollmentRepository;

        public LessonCompletionService(
            ILessonCompletionRepository completionRepository,
            ILessonRepository lessonRepository,
            IEnrolledCourseRepository enrollmentRepository)
        {
            _completionRepository = completionRepository;
            _lessonRepository = lessonRepository;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task CompleteLessonAsync(int userId, int lessonId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                throw new KeyNotFoundException("Lesson not found");

            bool isEnrolled = await _enrollmentRepository
                .IsUserEnrolledAsync(userId, lesson.CourseId);

            if (!isEnrolled)
                throw new UnauthorizedAccessException(
                    "You are not enrolled in this course"
                );

            bool alreadyCompleted = await _completionRepository
                .IsLessonCompletedAsync(userId, lessonId);

            if (alreadyCompleted)
                throw new InvalidOperationException(
                    "Lesson already completed"
                );

            var completion = new LessonCompletion
            {
                UserId = userId,
                LessonId = lessonId,
                CompletedDate = DateTime.UtcNow
            };

            await _completionRepository.AddAsync(completion);
        }

        public async Task<IEnumerable<LessonCompletion>> GetCompletedLessonsAsync(
            int userId)
        {
            return await _completionRepository
                .GetByUserIdAsync(userId);
        }
    }
}
