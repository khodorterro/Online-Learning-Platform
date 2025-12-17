using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearning.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSubmittedToQuizAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "QuizAttempts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "QuizAttempts");
        }
    }
}
