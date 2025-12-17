using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public  interface IEnrolledCourseRepository
    {
        Task<bool> IsEnrolledAsync(int userId, int courseId);
        Task AddAsync(EnrolledCourse enrolledCourse);
        Task<IEnumerable<EnrolledCourse>> GetByUserIdAsync(int userId);
    }
}
