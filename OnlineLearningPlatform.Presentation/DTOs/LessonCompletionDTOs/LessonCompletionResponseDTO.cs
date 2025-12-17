namespace OnlineLearningPlatform.Presentation.DTOs.LessonCompletionDTOs
{
    public class LessonCompletionResponseDTO
    {  
        public int LessonId { get; set; }
        public string LessonTitle { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
