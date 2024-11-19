using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatooMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTattoPriceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tattoosPrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TattooSize = table.Column<int>(type: "int", nullable: false),
                    BodyPlacement = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    StudioId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tattoosPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tattoosPrice_studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 18, 17, 13, 39, 278, DateTimeKind.Utc).AddTicks(6278));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 18, 17, 13, 39, 278, DateTimeKind.Utc).AddTicks(6294));

            migrationBuilder.CreateIndex(
                name: "IX_tattoosPrice_StudioId",
                table: "tattoosPrice",
                column: "StudioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tattoosPrice");

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
        }
    }
}
