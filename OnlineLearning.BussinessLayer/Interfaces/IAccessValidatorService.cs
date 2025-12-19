using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface IAccessValidatorService
    {
        Task ValidateCourseAccessAsync(int userId, string role, int courseId);
    }
}
