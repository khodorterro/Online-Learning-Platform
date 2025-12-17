using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IEnrolledCourseService
    {
        Task EnrollAsync(int userId, int courseId);
        Task<IEnumerable<EnrolledCourse>> GetUserEnrollmentsAsync(int userId);
    }
}
