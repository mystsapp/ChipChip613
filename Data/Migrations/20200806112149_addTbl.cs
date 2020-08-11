using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DVT2",
                table: "NhapHangs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DVT2",
                table: "NhapHangs");
        }
    }
}
