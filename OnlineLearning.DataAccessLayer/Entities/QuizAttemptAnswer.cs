using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class QuizAttemptAnswer
{
    public int Id { get; set; }

    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int SelectedAnswerId { get; set; }

    public bool IsCorrect { get; set; }

    public DateTime AnsweredAt { get; set; }

    public virtual QuizAttempt Attempt { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Answer SelectedAnswer { get; set; } = null!;
}
