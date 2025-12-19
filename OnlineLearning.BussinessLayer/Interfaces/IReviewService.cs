using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IReviewService
    {
        Task<Review> CreateAsync(int userId,int courseId,int rating,string? comment);
        Task<IEnumerable<Review>> GetByCourseIdAsync(int courseId);
    }
}
