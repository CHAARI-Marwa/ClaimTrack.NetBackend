using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimTrack.NetBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesVendus_Users_UserId",
                table: "ArticlesVendus");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesVendus_UserId",
                table: "ArticlesVendus");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ArticlesVendus");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesVendus_IdUser",
                table: "ArticlesVendus",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesVendus_Users_IdUser",
                table: "ArticlesVendus",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesVendus_Users_IdUser",
                table: "ArticlesVendus");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesVendus_IdUser",
                table: "ArticlesVendus");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ArticlesVendus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesVendus_UserId",
                table: "ArticlesVendus",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesVendus_Users_UserId",
                table: "ArticlesVendus",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
