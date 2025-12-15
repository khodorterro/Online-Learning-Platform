namespace OnlineLearningPlatform.Presentation.DTOs
{
    public class RegisterUserDTO
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "Student";
    }
}
