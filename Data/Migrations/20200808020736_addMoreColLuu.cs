using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addMoreColLuu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DVT2Luu",
                table: "NhapHangs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoLuong2Luu",
                table: "NhapHangs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DVT2Luu",
                table: "NhapHangs");

            migrationBuilder.DropColumn(
                name: "SoLuong2Luu",
                table: "NhapHangs");
        }
    }
}
