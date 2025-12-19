using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.Presentation.DTOs
{
    public class CreateReviewDTO
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}
