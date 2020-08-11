using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class changeTblName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguyenLieus");

            migrationBuilder.CreateTable(
                name: "ChiPhis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguyenLieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiNhap = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DTNoiNhap = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    DVT = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DonGia = table.Column<decimal>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgayNhap = table.Column<DateTime>(nullable: false),
                    ChiPhiKhac = table.Column<decimal>(nullable: false),
                    ThanhTien = table.Column<decimal>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "NguyenLieus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiPhiKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTNoiNhap = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    DVT = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoiNhap = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TenNguyenLieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguyenLieus", x => x.Id);
                });
        }
    }
}
