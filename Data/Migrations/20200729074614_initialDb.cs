using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DienThoai = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguyenLieus",
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
                    table.PrimaryKey("PK_NguyenLieus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonGia = table.Column<decimal>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    ThanhTien = table.Column<decimal>(nullable: false),
                    DonHangId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonHangs_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_DonHangId",
                table: "ChiTietDonHangs",
                column: "DonHangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "NguyenLieus");

            migrationBuilder.DropTable(
                name: "DonHangs");
        }
    }
}
