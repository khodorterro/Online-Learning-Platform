namespace OnlineLearningPlatform.Presentation.DTOs.QuestionDTOs
{
    public class CreateQuestionDTO
    {
        public int QuizId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
    }
}
