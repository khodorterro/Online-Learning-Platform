using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<IEnumerable<Course>> GetPublishedAsync();
        Task<Course?> UpdateAsync(int id,string title,string shortDescription,string longDescription,
            string difficulty,string thumbnail,bool isPublished);

        Task<Course> CreateAsync(string title, string shortDescription, string longDescription, int categoryId,
            string difficulty, string thumbnail, int createdBy);


    }
}
