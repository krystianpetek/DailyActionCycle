using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyActionCycle.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsNotified = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActionTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    DayDate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActionTemplates_ActionTemplateId",
                        column: x => x.ActionTemplateId,
                        principalTable: "ActionTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_Days_DayDate",
                        column: x => x.DayDate,
                        principalTable: "Days",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActionTemplateId",
                table: "Activities",
                column: "ActionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayDate",
                table: "Activities",
                column: "DayDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DayDate = table.Column<string>(type: "text", nullable: true),
                    DayDate1 = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActionTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    Daily = table.Column<bool>(type: "boolean", nullable: true),
                    Weekly = table.Column<bool>(type: "boolean", nullable: true),
                    ToDo_ActionTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notify = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entity_ActionTemplates_ActionTemplateId",
                        column: x => x.ActionTemplateId,
                        principalTable: "ActionTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entity_ActionTemplates_ToDo_ActionTemplateId",
                        column: x => x.ToDo_ActionTemplateId,
                        principalTable: "ActionTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entity_Days_DayDate",
                        column: x => x.DayDate,
                        principalTable: "Days",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entity_Days_DayDate1",
                        column: x => x.DayDate1,
                        principalTable: "Days",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ActionTemplateId",
                table: "Entity",
                column: "ActionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_DayDate",
                table: "Entity",
                column: "DayDate");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_DayDate1",
                table: "Entity",
                column: "DayDate1");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ToDo_ActionTemplateId",
                table: "Entity",
                column: "ToDo_ActionTemplateId");
        }
    }
}
