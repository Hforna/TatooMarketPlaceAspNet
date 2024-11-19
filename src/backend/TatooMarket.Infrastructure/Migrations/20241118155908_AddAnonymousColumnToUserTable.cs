using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatooMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnonymousColumnToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_CustomerId",
                table: "reviews");

            migrationBuilder.AddColumn<float>(
                name: "Note",
                table: "tattoos",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "reviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 18, 15, 59, 7, 730, DateTimeKind.Utc).AddTicks(7391));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 18, 15, 59, 7, 730, DateTimeKind.Utc).AddTicks(7411));

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_CustomerId",
                table: "reviews",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_CustomerId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "tattoos");

            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "reviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_CustomerId",
                table: "reviews",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
