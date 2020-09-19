using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixChiPhiTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "ChiPhis",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySua",
                table: "ChiPhis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTaoLuu",
                table: "ChiPhis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NguoiSua",
                table: "ChiPhis",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "ChiPhis");

            migrationBuilder.DropColumn(
                name: "NgaySua",
                table: "ChiPhis");

            migrationBuilder.DropColumn(
                name: "NgayTaoLuu",
                table: "ChiPhis");

            migrationBuilder.DropColumn(
                name: "NguoiSua",
                table: "ChiPhis");
        }
    }
}
