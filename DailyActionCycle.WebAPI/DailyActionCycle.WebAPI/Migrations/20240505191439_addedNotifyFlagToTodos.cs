using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyActionCycle.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedNotifyFlagToTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                table: "Entity",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notify",
                table: "Entity");
        }
    }
}
