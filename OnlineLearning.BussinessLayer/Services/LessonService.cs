using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;

namespace OnlineLearning.BusinessLayer.Services
{
    public class LessonService : IlessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAccessValidatorService _accessValidatorService;
        private readonly IEnrolledCourseRepository _enrollmentRepository;

        public LessonService(
            ILessonRepository lessonRepository,
            ICourseRepository courseRepository,
            IAccessValidatorService accessValidatorService,
            IEnrolledCourseRepository enrollmentRepository)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _accessValidatorService = accessValidatorService;
            _enrollmentRepository = enrollmentRepository;
        }

        private async Task ValidateLessonOwnershipAsync(
            int lessonId,
            int instructorId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                throw new KeyNotFoundException("Lesson not found");

            var course = await _courseRepository.GetByIdAsync(lesson.CourseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (course.CreatedBy != instructorId)
                throw new UnauthorizedAccessException(
                    "You are not allowed to manage this lesson"
                );
        }

        private async Task ValidateCourseOwnershipAsync(
            int courseId,
            int instructorId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (course.CreatedBy != instructorId)
                throw new UnauthorizedAccessException(
                    "You are not allowed to manage this course"
                );
        }

        public async Task<Lesson?> GetByIdAsync(int id)
        {
            return await _lessonRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(
            int courseId,
            int userId,
            string role)
        {
            await _accessValidatorService.ValidateCourseAccessAsync(
                userId,
                role,
                courseId
            );

            return await _lessonRepository.GetByCourseIdAsync(courseId);
        }

        public async Task<Lesson> GetByIdWithAccessAsync(
            int lessonId,
            int userId,
            string role)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                throw new KeyNotFoundException("Lesson not found");

            var course = await _courseRepository.GetByIdAsync(lesson.CourseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            // ADMIN → always allowed
            if (role == "Admin")
                return lesson;

            // INSTRUCTOR → must own course
            if (role == "Instructor")
            {
                if (course.CreatedBy != userId)
                    throw new UnauthorizedAccessException(
                        "You do not own this course"
                    );
                return lesson;
            }

            bool isEnrolled = await _enrollmentRepository
                .IsUserEnrolledAsync(userId, course.Id);

            if (!isEnrolled)
                throw new UnauthorizedAccessException(
                    "You are not enrolled in this course"
                );

            return lesson;
        }


        public async Task<Lesson> AddAsync(
            int courseId,
            string title,
            string content,
            string? videoUrl,
            int order,
            int? estimatedDuration,
            int instructorId)
        {
            await ValidateCourseOwnershipAsync(courseId, instructorId);

            var lesson = new Lesson
            {
                CourseId = courseId,
                Title = title,
                Content = content,
                VideoUrl = videoUrl,
                Order = order,
                EstimatedDuration = estimatedDuration
            };

            await _lessonRepository.AddAsync(lesson);
            return lesson;
        }

        public async Task<Lesson?> UpdateAsync(
            int lessonId,
            string title,
            string content,
            string? videoUrl,
            int order,
            int? estimatedDuration,
            int instructorId)
        {
            await ValidateLessonOwnershipAsync(lessonId, instructorId);

            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                return null;

            lesson.Title = title;
            lesson.Content = content;
            lesson.VideoUrl = videoUrl;
            lesson.Order = order;
            lesson.EstimatedDuration = estimatedDuration;

            await _lessonRepository.UpdateAsync(lesson);
            return lesson;
        }

        public async Task<bool> DeleteAsync(
            int lessonId,
            int instructorId)
        {
            await ValidateLessonOwnershipAsync(lessonId, instructorId);

            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            if (lesson == null)
                return false;

            await _lessonRepository.DeleteAsync(lesson);
            return true;
        }
    }
}
