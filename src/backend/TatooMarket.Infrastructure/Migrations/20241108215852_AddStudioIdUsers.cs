using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatooMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudioIdUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_studios_OwnerId",
                table: "studios");

            migrationBuilder.AddColumn<long>(
                name: "StudioId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_studios_OwnerId",
                table: "studios",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudioId",
                table: "AspNetUsers",
                column: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_studios_StudioId",
                table: "AspNetUsers",
                column: "StudioId",
                principalTable: "studios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_studios_StudioId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_studios_OwnerId",
                table: "studios");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudioId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_studios_OwnerId",
                table: "studios",
                column: "OwnerId");
        }
    }
}
