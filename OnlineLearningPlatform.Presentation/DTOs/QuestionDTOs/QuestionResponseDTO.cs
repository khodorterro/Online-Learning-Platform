namespace OnlineLearningPlatform.Presentation.DTOs.QuestionDTOs
{
    public class QuestionResponseDTO
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
    }
}
