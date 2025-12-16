namespace OnlineLearningPlatform.Presentation.DTOs.CoursesDTOs
{
    public class CreateCourseDTO
    {
        public string Title { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Difficulty { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public int CreatedBy { get; set; }
    }
}
