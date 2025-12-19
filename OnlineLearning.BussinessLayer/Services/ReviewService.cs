using OnlineLearning.DataAccessLayer.Entities;
using OnlineLearning.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BusinessLayer.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IEnrolledCourseRepository _enrollmentRepo;

        public ReviewService(
            IReviewRepository reviewRepo,
            IEnrolledCourseRepository enrollmentRepo)
        {
            _reviewRepo = reviewRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public async Task<Review> CreateAsync(int userId,int courseId,int rating,string? comment)
        {
            // 1️⃣ Must be enrolled
            if (!await _enrollmentRepo.IsUserEnrolledAsync(userId, courseId))
                throw new UnauthorizedAccessException(
                    "You must enroll before reviewing"
                );

            // 2️⃣ One review only
            if (await _reviewRepo.ExistsAsync(userId, courseId))
                throw new InvalidOperationException(
                    "You already reviewed this course"
                );

            var review = new Review
            {
                UserId = userId,
                CourseId = courseId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepo.AddAsync(review);
            return review;
        }

        public Task<IEnumerable<Review>> GetByCourseIdAsync(int courseId)
        {
           return  _reviewRepo.GetByCourseIdAsync(courseId);
        }
    }
}
