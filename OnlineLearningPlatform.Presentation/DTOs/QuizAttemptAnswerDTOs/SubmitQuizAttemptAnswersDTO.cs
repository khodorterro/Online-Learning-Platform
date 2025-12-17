namespace OnlineLearningPlatform.Presentation.DTOs.QuizAttemptAnswerDTOs
{
    public class SubmitQuizAttemptAnswersDTO
    {
        public int AttemptId { get; set; }
        public List<CreateQuizAttemptAnswerDTO> Answers { get; set; } = new();
    }
}
