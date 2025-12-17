using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface ICourseProgressService
    {
        Task<int> GetCourseProgressAsync(int userId, int courseId);
    }
}
