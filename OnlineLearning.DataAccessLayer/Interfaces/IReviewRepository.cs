using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);

        Task<bool> ExistsAsync(int userId, int courseId);

        Task<IEnumerable<Review>> GetByCourseIdAsync(int courseId);
    }
}
