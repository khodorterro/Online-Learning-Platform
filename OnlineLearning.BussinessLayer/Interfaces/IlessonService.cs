using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public interface IlessonService
    {
        Task<Lesson?>GetByIdAsync(int id);
        Task<IEnumerable<Lesson>>GetByCourseIdAsync(int courseId, int userId, string role);
        Task<Lesson> AddAsync(int courseId,string title,string content,string? videoUrl,int order,int? estimatedDuration,int instructorId);
        Task<Lesson?> UpdateAsync(int id, string title, string content, string? videoUrl,int order, int? estimatedDuration, int instructorId);
        Task<bool> DeleteAsync(int id, int instructorId);
        Task<Lesson> GetByIdWithAccessAsync(int lessonId,int userId,string role);

    }
}
