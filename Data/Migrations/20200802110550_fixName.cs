using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenNguyenLieu",
                table: "ChiPhis",
                newName: "TenChiPhi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenChiPhi",
                table: "ChiPhis",
                newName: "TenNguyenLieu");
        }
    }
}
