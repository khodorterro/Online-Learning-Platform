namespace OnlineLearningPlatform.Presentation.DTOs.QuizAttemptDTOs
{
    public class QuizAttemptResponseDTO
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}
