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
        Task<IEnumerable<Lesson>>GetByCourseIdAsync(int courseId);
        Task<Lesson> AddAsync(int courseId,string title,string content,string? videoUrl,int order,int? estimatedDuration);
        Task<Lesson?> UpdateAsync(int id, string title, string content, string? videoUrl,int order, int? estimatedDuration);
        Task<bool> DeleteAsync(int id);
    }
}
