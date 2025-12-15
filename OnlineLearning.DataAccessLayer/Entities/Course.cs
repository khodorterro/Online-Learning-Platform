using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public string? Difficulty { get; set; }

    public string? Thumbnail { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsPublished { get; set; }

    public int? CategoryId { get; set; }

    public virtual CourseCategory? Category { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<EnrolledCourse> EnrolledCourses { get; set; } = new List<EnrolledCourse>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
