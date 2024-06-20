using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holy_Man_API.Migrations
{
    /// <inheritdoc />
    public partial class hj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserModelId",
                table: "Messages",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserModelId",
                table: "Messages",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserModelId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserModelId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Messages");
        }
    }
}
