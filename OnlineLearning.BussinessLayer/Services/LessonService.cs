using Microsoft.VisualBasic;
using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class LessonService:IlessonService
    {
        private readonly ILessonRepository _lessonRepository;
        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Lesson?>GetByIdAsync(int id)
        {
            return await _lessonRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId)
        {
            return await _lessonRepository.GetByCourseIdAsync(courseId);
        }

        public async Task<bool>DeleteAsync(int id)
        {
            var lesson= await _lessonRepository.GetByIdAsync(id);
            if(lesson == null)
                return false;

            await _lessonRepository.DeleteAsync(id);
            return true;
        }
        public async Task<Lesson?> UpdateAsync(int id, string title, string content,
            string? videoUrl,int order, int? estimatedDuration)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
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

        public async Task<Lesson> AddAsync(int courseId, string title, string content,
            string? videoUrl, int order, int? estimatedDuration)
        {
            var lesson = new Lesson
            {
                Content = content,
                VideoUrl = videoUrl,
                Order = order,
                EstimatedDuration = estimatedDuration,
                Title = title,
                CourseId = courseId
            };

            await _lessonRepository.AddAsync(lesson);
            return lesson;
        }


    }
}
