namespace OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs
{
    public class CourseResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; } = null!;
        public string? Difficulty { get; set; } = null!;
        public bool IsPublished { get; set; }
    }
}
