namespace OnlineLearningPlatform.Presentation.DTOs.LessonDTOs
{
    public class CreateLessonDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public int? EstimatedDuration { get; set; }
    }
}
