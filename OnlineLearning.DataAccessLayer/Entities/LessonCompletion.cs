using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class LessonCompletion
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int UserId { get; set; }

    public DateTime CompletedDate { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
