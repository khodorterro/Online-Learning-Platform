using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
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
        public CourseService(ICourseRepository courseRepository)
        {
            this._CourseRepository = courseRepository;
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _CourseRepository.GetByIdAsync(id);
        }

        public async Task<Course> CreateAsync(string title, string shortDescription, string longDescription, int categoryId,
            string difficulty, string thumbnail, int createdBy)
        {
            var course = new Course
            {
                Title = title,
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                CreatedBy = createdBy,
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
            string difficulty, string thumbnail, bool isPublished)
        {
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
    }
}
