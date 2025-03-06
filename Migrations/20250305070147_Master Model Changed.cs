using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecom.Migrations
{
    /// <inheritdoc />
    public partial class MasterModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "ProductOrderMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "ProductOrderMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "ProductOrderMaster");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "ProductOrderMaster");
        }
    }
}
