namespace OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs
{
    public class UpdateCourseDTO
    {
        public string Title { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public string Difficulty { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public bool IsPublished { get; set; }
    }
}
