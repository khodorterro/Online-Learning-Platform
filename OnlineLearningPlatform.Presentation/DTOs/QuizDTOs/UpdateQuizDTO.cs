namespace OnlineLearningPlatform.Presentation.DTOs.QuizDTOs
{
    public class UpdateQuizDTO
    {
        public string Title { get; set; } = null!;
        public int PassingScore { get; set; }
        public int? TimeLimit { get; set; }
    }
}
