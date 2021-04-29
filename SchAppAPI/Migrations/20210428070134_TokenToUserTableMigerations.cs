using Microsoft.EntityFrameworkCore.Migrations;

namespace SchAppAPI.Migrations
{
    public partial class TokenToUserTableMigerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "AspNetUsers");
        }
    }
}
