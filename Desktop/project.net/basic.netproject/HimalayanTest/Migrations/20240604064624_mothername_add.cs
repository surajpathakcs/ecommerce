using Microsoft.EntityFrameworkCore.Migrations;

namespace HimalayanTest.Migrations
{
    public partial class mothername_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Students");
        }
    }
}
