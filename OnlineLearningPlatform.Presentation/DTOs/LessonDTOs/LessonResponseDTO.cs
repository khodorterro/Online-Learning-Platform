namespace OnlineLearningPlatform.Presentation.DTOs.LessonDTOs
{
    public class LessonResponseDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public int? EstimatedDuration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
