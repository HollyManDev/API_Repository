using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holy_Man_API.Migrations
{
    /// <inheritdoc />
    public partial class Thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Conversations");
        }
    }
}
