namespace OnlineLearningPlatform.Presentation.DTOs.LessonDTOs
{
    public class UpdateLessonDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public int? EstimatedDuration { get; set; }
    }
}
