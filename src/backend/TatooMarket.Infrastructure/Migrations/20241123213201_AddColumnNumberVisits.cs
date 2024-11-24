using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatooMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNumberVisits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NumberVisits",
                table: "studios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 23, 21, 32, 0, 765, DateTimeKind.Utc).AddTicks(7814));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 23, 21, 32, 0, 765, DateTimeKind.Utc).AddTicks(7849));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberVisits",
                table: "studios");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 22, 3, 41, 19, 397, DateTimeKind.Utc).AddTicks(5021));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 22, 3, 41, 19, 397, DateTimeKind.Utc).AddTicks(5039));
        }
    }
}
