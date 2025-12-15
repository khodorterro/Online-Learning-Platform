using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string? Comment { get; set; }

    public int? Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
