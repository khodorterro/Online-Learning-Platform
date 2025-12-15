using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<EnrolledCourse> EnrolledCourses { get; set; } = new List<EnrolledCourse>();

    public virtual ICollection<LessonCompletion> LessonCompletions { get; set; } = new List<LessonCompletion>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
