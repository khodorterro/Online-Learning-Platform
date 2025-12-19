namespace OnlineLearningPlatform.Presentation.DTOs.QuizDTOs
{
    public class ReviewResponseDTO
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
    }
}
