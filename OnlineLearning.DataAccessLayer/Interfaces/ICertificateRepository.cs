using OnlineLearning.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Interfaces
{
    public  interface ICertificateRepository
    {
        Task AddAsync(Certificate certificate);

        Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId);

        Task<IEnumerable<Certificate>> GetByUserIdAsync(int userId);
    }
}
