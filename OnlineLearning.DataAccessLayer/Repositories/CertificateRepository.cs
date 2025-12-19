using Microsoft.EntityFrameworkCore;
using OnlineLearning.DataAccessLayer.Context;
using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.DataAccessLayer.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly AppDbContext _context;

        public CertificateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
        }


        public async Task<Certificate?> GetByUserAndCourseAsync(int userId,int courseId)
        {
            return await _context.Certificates.FirstOrDefaultAsync(c =>c.UserId == userId && c.CourseId == courseId);
        }

        public async Task<IEnumerable<Certificate>> GetByUserIdAsync(int userId)
        {
            return await _context.Certificates
                .Where(c => c.UserId == userId)
                .Include(c => c.Course)
                .OrderByDescending(c => c.GeneratedAt)
                .ToListAsync();
        }
    }
}
