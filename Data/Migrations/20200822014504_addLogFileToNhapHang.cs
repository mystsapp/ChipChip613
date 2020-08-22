using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addLogFileToNhapHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "NhapHangs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "NhapHangs");
        }
    }
}
