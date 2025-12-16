namespace OnlineLearningPlatform.Presentation.DTOs.AnswerDTOs
{
    public class CreateAnswerDTO
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; } = null!;
        public bool IsCorrect { get; set; }
    }
}
