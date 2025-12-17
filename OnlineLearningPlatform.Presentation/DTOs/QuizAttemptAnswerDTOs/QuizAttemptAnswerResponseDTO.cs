namespace OnlineLearningPlatform.Presentation.DTOs.QuizAttemptAnswerDTOs
{
    public class QuizAttemptAnswerResponseDTO
    {
        public int Id { get; set; }
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
