using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyActionCycle.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Date = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    DayDate = table.Column<string>(type: "text", nullable: true),
                    DayDate1 = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Daily = table.Column<bool>(type: "boolean", nullable: true),
                    Weekly = table.Column<bool>(type: "boolean", nullable: true),
                    ActionTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ToDo_ActionTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "ActionTemplates");

            migrationBuilder.DropTable(
                name: "Days");
        }
    }
}
