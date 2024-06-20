using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holy_Man_API.Migrations
{
    /// <inheritdoc />
    public partial class w10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Messages_MessageModelId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Conversations_ConversationModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ConversationModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Documents_MessageModelId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ConversationModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MessageModelId",
                table: "Documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConversationModelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageModelId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConversationModelId",
                table: "Users",
                column: "ConversationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_MessageModelId",
                table: "Documents",
                column: "MessageModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Messages_MessageModelId",
                table: "Documents",
                column: "MessageModelId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Conversations_ConversationModelId",
                table: "Users",
                column: "ConversationModelId",
                principalTable: "Conversations",
                principalColumn: "Id");
        }
    }
}
