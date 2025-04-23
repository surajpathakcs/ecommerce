﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecom.Migrations
{
    /// <inheritdoc />
    public partial class twomodelsforcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductOrderMaster",
                columns: table => new
                {
                    ProductOrderMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderMaster", x => x.ProductOrderMasterID);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDetail",
                columns: table => new
                {
                    ProductOrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductOrderMasterID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductItemId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDetail", x => x.ProductOrderDetailID);
                    table.ForeignKey(
                        name: "FK_ProductOrderDetail_ProductItem_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItem",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDetail_ProductOrderMaster_ProductOrderMasterID",
                        column: x => x.ProductOrderMasterID,
                        principalTable: "ProductOrderMaster",
                        principalColumn: "ProductOrderMasterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDetail_ProductItemId",
                table: "ProductOrderDetail",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDetail_ProductOrderMasterID",
                table: "ProductOrderDetail",
                column: "ProductOrderMasterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrderDetail");

            migrationBuilder.DropTable(
                name: "ProductOrderMaster");
        }
    }
}
