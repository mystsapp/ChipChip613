using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addColNH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DVTLuu",
                table: "NhapHangs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoLuongLuu",
                table: "NhapHangs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ThanhTienLuu",
                table: "NhapHangs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ChiPhis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhapHangId = table.Column<long>(nullable: false),
                    ChiPhiKhac = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DVT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DVT2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    SoLuong2 = table.Column<int>(nullable: false),
                    DonGia = table.Column<decimal>(nullable: false),
                    ThanhTien = table.Column<decimal>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiPhis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiPhis");

            migrationBuilder.DropColumn(
                name: "DVTLuu",
                table: "NhapHangs");

            migrationBuilder.DropColumn(
                name: "SoLuongLuu",
                table: "NhapHangs");

            migrationBuilder.DropColumn(
                name: "ThanhTienLuu",
                table: "NhapHangs");
        }
    }
}
