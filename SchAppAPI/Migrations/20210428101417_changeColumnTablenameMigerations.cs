using Microsoft.EntityFrameworkCore.Migrations;

namespace SchAppAPI.Migrations
{
    public partial class changeColumnTablenameMigerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "token",
                table: "AspNetUsers",
                newName: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "AspNetUsers",
                newName: "token");
        }
    }
}
