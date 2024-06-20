using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holy_Man_API.Migrations
{
    /// <inheritdoc />
    public partial class t20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_MessageId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Documents",
                newName: "ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_MessageId",
                table: "Documents",
                newName: "IX_Documents_ConversationId");

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
                name: "FK_Documents_Conversations_ConversationId",
                table: "Documents",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id");

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
                name: "FK_Documents_Conversations_ConversationId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserModelId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserModelId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "Documents",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_ConversationId",
                table: "Documents",
                newName: "IX_Documents_MessageId");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserModelId",
                table: "Users",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_MessageId",
                table: "Documents",
                column: "MessageId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserModelId",
                table: "Users",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
