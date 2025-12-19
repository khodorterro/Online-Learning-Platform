using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Interfaces
{
    public  interface ICertificateService
    {
        Task<Certificate> GenerateAsync(int userId, int courseId);
        Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId);
        Task<IEnumerable<Certificate>> GetUserCertificatesAsync(int userId);
    }
}
