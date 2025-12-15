using System;
using System.Collections.Generic;

namespace OnlineLearning.DataAccessLayer.Entities;

public partial class Answer
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string? AnswerText { get; set; }

    public bool IsCorrect { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<QuizAttemptAnswer> QuizAttemptAnswers { get; set; } = new List<QuizAttemptAnswer>();
}
