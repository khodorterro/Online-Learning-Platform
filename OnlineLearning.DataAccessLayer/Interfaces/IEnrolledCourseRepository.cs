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
        Task<bool> IsUserEnrolledAsync(int userId, int courseId);
        Task<List<int>> GetCourseIdsByUserAsync(int userId);
        Task AddAsync(EnrolledCourse enrollment);
        Task<bool> IsInstructorOwnerOfCourseAsync(int instructorId, int courseId);
        Task<IEnumerable<EnrolledCourse>> GetByUserIdAsync(int userId);
    }
}
