using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchAppAPI.Migrations
{
    public partial class LessonSchemaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Contents_ContentId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Quizzes_QuizId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ContentId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_QuizId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Lessons");

            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "Contents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_LessonId",
                table: "Quizzes",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_LessonId",
                table: "Contents",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Lessons_LessonId",
                table: "Contents",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Lessons_LessonId",
                table: "Quizzes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Lessons_LessonId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Lessons_LessonId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_LessonId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Contents_LessonId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Contents");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuizId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ContentId",
                table: "Lessons",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_QuizId",
                table: "Lessons",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Contents_ContentId",
                table: "Lessons",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Quizzes_QuizId",
                table: "Lessons",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
