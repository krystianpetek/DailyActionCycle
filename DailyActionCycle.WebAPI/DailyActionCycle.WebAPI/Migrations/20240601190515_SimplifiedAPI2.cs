using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyActionCycle.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedAPI2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Days_DayDate",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_DayDate",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "DayDate",
                table: "Activities");

            migrationBuilder.CreateTable(
                name: "ActivityDay",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDay", x => new { x.ActivitiesId, x.DayDate });
                    table.ForeignKey(
                        name: "FK_ActivityDay_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityDay_Days_DayDate",
                        column: x => x.DayDate,
                        principalTable: "Days",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDay_DayDate",
                table: "ActivityDay",
                column: "DayDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDay");

            migrationBuilder.AddColumn<string>(
                name: "DayDate",
                table: "Activities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayDate",
                table: "Activities",
                column: "DayDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Days_DayDate",
                table: "Activities",
                column: "DayDate",
                principalTable: "Days",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
