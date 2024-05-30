using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyActionCycle.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addedactiontemplatetoday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActionTemplateId",
                table: "Days",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_ActionTemplateId",
                table: "Days",
                column: "ActionTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_ActionTemplates_ActionTemplateId",
                table: "Days",
                column: "ActionTemplateId",
                principalTable: "ActionTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_ActionTemplates_ActionTemplateId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_ActionTemplateId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "ActionTemplateId",
                table: "Days");
        }
    }
}
