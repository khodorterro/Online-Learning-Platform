using OnlineLearning.BusinessLayer.Interfaces;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class CertificateService
    {
        private readonly ICertificateRepository _certificateRepo;
        private readonly ICourseProgressService _progressService;

        public CertificateService(
            ICertificateRepository certificateRepo,
            ICourseProgressService progressService)
        {
            _certificateRepo = certificateRepo;
            _progressService = progressService;
        }

        public async Task<Certificate> GenerateAsync(int userId, int courseId)
        {

            var existing = await _certificateRepo.GetByUserAndCourseAsync(userId, courseId);

            if (existing != null)
                return existing;


            int progress = await _progressService.GetCourseProgressAsync(userId, courseId);

            if (progress < 100)
                throw new InvalidOperationException(
                    "Course not completed yet"
                );

            var certificate = new Certificate
            {
                UserId = userId,
                CourseId = courseId,
                GeneratedAt = DateTime.UtcNow,
                DownloadUrl = $"certificates/{userId}_{courseId}.pdf"
            };

            await _certificateRepo.AddAsync(certificate);
            return certificate;
        }

        public Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId)
        {
           return _certificateRepo.GetByUserAndCourseAsync(userId, courseId);
        }

        public Task<IEnumerable<Certificate>> GetUserCertificatesAsync(int userId)
        {
            return _certificateRepo.GetByUserIdAsync(userId);
        }
    }
}
