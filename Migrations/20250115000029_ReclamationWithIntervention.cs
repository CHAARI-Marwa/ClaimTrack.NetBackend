using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimTrack.NetBackend.Migrations
{
    /// <inheritdoc />
    public partial class ReclamationWithIntervention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Reclamations_ReclamationId",
                table: "Interventions");

            migrationBuilder.DropIndex(
                name: "IX_Interventions_ReclamationId",
                table: "Interventions");

            migrationBuilder.DropColumn(
                name: "ReclamationId",
                table: "Interventions");

            migrationBuilder.AddColumn<int>(
                name: "IdIntervention",
                table: "Reclamations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_IdIntervention",
                table: "Reclamations",
                column: "IdIntervention",
                unique: true,
                filter: "[IdIntervention] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Interventions_IdIntervention",
                table: "Reclamations",
                column: "IdIntervention",
                principalTable: "Interventions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Interventions_IdIntervention",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_IdIntervention",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "IdIntervention",
                table: "Reclamations");

            migrationBuilder.AddColumn<int>(
                name: "ReclamationId",
                table: "Interventions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ReclamationId",
                table: "Interventions",
                column: "ReclamationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Reclamations_ReclamationId",
                table: "Interventions",
                column: "ReclamationId",
                principalTable: "Reclamations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
