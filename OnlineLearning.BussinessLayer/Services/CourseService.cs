using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _CourseRepository;
        private readonly IEnrolledCourseRepository _EnrolledCourseRepository;
        public CourseService(ICourseRepository courseRepository,IEnrolledCourseRepository enrolledCourseRepository)
        {
            _CourseRepository = courseRepository;
            _EnrolledCourseRepository = enrolledCourseRepository;
            
        }
        private async Task ValidateInstructorOwnershipAsync(int courseId, int instructorId)
        {
            var course = await _CourseRepository.GetByIdAsync(courseId);

            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (course.CreatedBy != instructorId)
                throw new UnauthorizedAccessException(
                    "You are not allowed to manage this course"
                );
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _CourseRepository.GetByIdAsync(id);
        }

        public async Task<Course> CreateAsync(string title, string shortDescription, string longDescription, int categoryId,
            string difficulty, string thumbnail,int InstructorId)
        {
            var course = new Course
            {
                Title = title,
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                CreatedBy = InstructorId,
                CategoryId = categoryId,
                Difficulty = difficulty,
                Thumbnail = thumbnail,
            };
            await _CourseRepository.AddAsync(course);
            return course;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _CourseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> GetPublishedAsync()
        {
            return await _CourseRepository.GetPublichedAsync();
        }
        public async Task<Course?> UpdateAsync(int id, string title, string shortDescription, string longDescription,
            string difficulty, string thumbnail, bool isPublished,int InstructorId)
        {
            await ValidateInstructorOwnershipAsync(id, InstructorId);

            var course = await _CourseRepository.GetByIdAsync(id);
            if (course == null)
                return null;
            course.Title = title;
            course.ShortDescription = shortDescription;
            course.LongDescription = longDescription;
            course.Difficulty = difficulty;
            course.Thumbnail = thumbnail;
            course.IsPublished = isPublished;
            await _CourseRepository.UpdateAsync(course);
            return course;
        }
        public async Task<IEnumerable<Course>> GetAvailableCoursesAsync(int userId,string role)
        {

            if (role == "Admin")
            {
                return await _CourseRepository.GetAllAsync();
            }

            if (role == "Instructor")
            {
                return await _CourseRepository.GetAllAsync();
            }

            var publishedCourses = await _CourseRepository.GetPublichedAsync();

            var enrolledCourseIds = await _EnrolledCourseRepository
                .GetCourseIdsByUserAsync(userId);

            var result = publishedCourses
                .Where(c =>
                    c.IsPublished ||
                    enrolledCourseIds.Contains(c.Id))
                .Distinct()
                .ToList();

            return result;
        }

        public async Task<Course> GetByIdWithAccessAsync(int courseId,int userId,string role)
        {
            var course = await _CourseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (role == "Admin")
                return course;

            if (role == "Instructor")
            {
                if (course.CreatedBy != userId)
                    throw new UnauthorizedAccessException(
                        "You do not own this course"
                    );

                return course;
            }

            bool isEnrolled = await  _EnrolledCourseRepository.IsUserEnrolledAsync (userId, courseId);

            if (!course.IsPublished && !isEnrolled)
                throw new UnauthorizedAccessException(
                    "You are not enrolled in this course"
                );

            return course;
        }


    }
}
