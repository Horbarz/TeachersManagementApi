using Microsoft.EntityFrameworkCore.Migrations;

namespace SchAppAPI.Migrations
{
    public partial class DetailsToColumnTableMigerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Subjects");
        }
    }
}
