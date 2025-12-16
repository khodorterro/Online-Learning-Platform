namespace OnlineLearningPlatform.Presentation.DTOs.QuizDTOs
{
    public class QuizResponseDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? LessonId { get; set; }
        public string Title { get; set; } = null!;
        public int PassingScore { get; set; }
        public int? TimeLimit { get; set; }
    }
}
