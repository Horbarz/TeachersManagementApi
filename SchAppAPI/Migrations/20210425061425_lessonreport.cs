using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchAppAPI.Migrations
{
    public partial class lessonreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonReports",
                columns: table => new
                {
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompletionRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSpentOnModule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonReports", x => new { x.LessonId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_LessonReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonReports_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizReports",
                columns: table => new
                {
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MarkObtained = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkObtainable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentageCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizUserAnswers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizReports", x => new { x.QuizId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_QuizReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizReports_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonReports_UserId",
                table: "LessonReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizReports_UserId",
                table: "QuizReports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonReports");

            migrationBuilder.DropTable(
                name: "QuizReports");
        }
    }
}
