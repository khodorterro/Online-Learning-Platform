using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class CourseCategoryService:ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }
        public async Task<IEnumerable<CourseCategory>> GetAllAsync()
        {
          return  await _courseCategoryRepository.GetAllAsync();
        }
        public async Task<CourseCategory?> GetByIdAsync(int id)
        {
            return await _courseCategoryRepository.GetByIdAsync(id);
        }
        public async Task<CourseCategory> CreateAsync(string name,string? description)
        {
            var existing= await _courseCategoryRepository.GetByNameAsync(name);
            if (existing != null)
                throw new Exception("already exist");
            var category= new CourseCategory
            {
                Name = name,
                Description = description,
                CreatedAt = DateTime.Now,
            };
            await _courseCategoryRepository.AddAsync(category);
            return category;
        }
        public async Task<CourseCategory?> UpdateAsync(int id,string name,string? description)
        {
            var coursecategory = await _courseCategoryRepository.GetByIdAsync(id);
            if (coursecategory == null)
                return null;
            coursecategory.Name = name;
            coursecategory.Description = description;
            await _courseCategoryRepository.UpdateAsync(coursecategory);
            return coursecategory;
        }

    }
}
