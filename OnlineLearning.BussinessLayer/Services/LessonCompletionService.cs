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
        private readonly ILessonCompletionRepository _lessonCompletionRepository;
        public LessonCompletionService(ILessonCompletionRepository lessonCompletionRepository)
        {
            _lessonCompletionRepository = lessonCompletionRepository;
        }
        public async Task CompleteLessonAsync(int userId, int lessonId)
        {
            if (await _lessonCompletionRepository.IsCompletedAsync(userId, lessonId))
                throw new InvalidOperationException("Lesson already completed");

            var completion = new LessonCompletion
            {
                UserId = userId,
                LessonId = lessonId,
                CompletedDate = DateTime.UtcNow
            };

            await _lessonCompletionRepository.AddAsync(completion);
        }
        public async Task<IEnumerable<LessonCompletion>> GetCompletedLessonsAsync(int userId)
        {
            return await _lessonCompletionRepository.GetByUserIdAsync(userId);
        }
    }
}
