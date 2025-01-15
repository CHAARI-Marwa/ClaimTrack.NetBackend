using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimTrack.NetBackend.Migrations
{
    /// <inheritdoc />
    public partial class Interventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateIntervention",
                table: "Interventions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duree",
                table: "Interventions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PieceRechangeId",
                table: "Interventions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReclamationId",
                table: "Interventions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Technicien",
                table: "Interventions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_PieceRechangeId",
                table: "Interventions",
                column: "PieceRechangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_PieceDetails_PieceRechangeId",
                table: "Interventions",
                column: "PieceRechangeId",
                principalTable: "PieceDetails",
                principalColumn: "PieceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_PieceDetails_PieceRechangeId",
                table: "Interventions");

            migrationBuilder.DropIndex(
                name: "IX_Interventions_PieceRechangeId",
                table: "Interventions");

            migrationBuilder.DropColumn(
                name: "DateIntervention",
                table: "Interventions");

            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Interventions");

            migrationBuilder.DropColumn(
                name: "PieceRechangeId",
                table: "Interventions");

            migrationBuilder.DropColumn(
                name: "ReclamationId",
                table: "Interventions");

            migrationBuilder.DropColumn(
                name: "Technicien",
                table: "Interventions");
        }
    }
}
