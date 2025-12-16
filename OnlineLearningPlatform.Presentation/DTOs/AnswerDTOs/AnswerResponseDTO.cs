namespace OnlineLearningPlatform.Presentation.DTOs.AnswerDTOs
{
    public class AnswerResponseDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; } = null!;
        public bool IsCorrect { get; set; }
    }
}
