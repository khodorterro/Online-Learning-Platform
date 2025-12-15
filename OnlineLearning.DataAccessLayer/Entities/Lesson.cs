using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class Lesson
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public string? VideoUrl { get; set; }

    public int Order { get; set; }

    public int? EstimatedDuration { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<LessonCompletion> LessonCompletions { get; set; } = new List<LessonCompletion>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
