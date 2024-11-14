using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatooMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerColumnToTattoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "tattoos",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 14, 3, 21, 28, 192, DateTimeKind.Utc).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 14, 3, 21, 28, 192, DateTimeKind.Utc).AddTicks(688));

            migrationBuilder.CreateIndex(
                name: "IX_tattoos_CustomerId",
                table: "tattoos",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_tattoos_AspNetUsers_CustomerId",
                table: "tattoos",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tattoos_AspNetUsers_CustomerId",
                table: "tattoos");

            migrationBuilder.DropIndex(
                name: "IX_tattoos_CustomerId",
                table: "tattoos");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "tattoos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 9, 3, 36, 23, 16, DateTimeKind.Utc).AddTicks(8663));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 9, 3, 36, 23, 16, DateTimeKind.Utc).AddTicks(8682));
        }
    }
}
