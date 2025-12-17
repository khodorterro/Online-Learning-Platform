namespace OnlineLearningPlatform.Presentation.DTOs.QuizAttemptAnswerDTOs
{
    public class AttemptQuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public List<AttemptAnswerDTO> Answers { get; set; } = new();

    }
}
