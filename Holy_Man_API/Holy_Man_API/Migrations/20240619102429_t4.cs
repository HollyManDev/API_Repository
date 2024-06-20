using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holy_Man_API.Migrations
{
    /// <inheritdoc />
    public partial class t4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_UserModel_UserId",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_UserModel_MessageId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_Conversations_ConversationModelId",
                table: "UserModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_UserModel_UserModelId",
                table: "UserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel");

            migrationBuilder.RenameTable(
                name: "UserModel",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UserModel_UserModelId",
                table: "Users",
                newName: "IX_Users_UserModelId");

            migrationBuilder.RenameIndex(
                name: "IX_UserModel_ConversationModelId",
                table: "Users",
                newName: "IX_Users_ConversationModelId");

            migrationBuilder.AddColumn<int>(
                name: "MessageModelId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    ConversationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_MessageModelId",
                table: "Documents",
                column: "MessageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_Users_UserId",
                table: "ConversationParticipants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Messages_MessageModelId",
                table: "Documents",
                column: "MessageModelId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_MessageId",
                table: "Documents",
                column: "MessageId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Conversations_ConversationModelId",
                table: "Users",
                column: "ConversationModelId",
                principalTable: "Conversations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserModelId",
                table: "Users",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_Users_UserId",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Messages_MessageModelId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_MessageId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Conversations_ConversationModelId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserModelId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Documents_MessageModelId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MessageModelId",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserModel");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserModelId",
                table: "UserModel",
                newName: "IX_UserModel_UserModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ConversationModelId",
                table: "UserModel",
                newName: "IX_UserModel_ConversationModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_UserModel_UserId",
                table: "ConversationParticipants",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_UserModel_MessageId",
                table: "Documents",
                column: "MessageId",
                principalTable: "UserModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_Conversations_ConversationModelId",
                table: "UserModel",
                column: "ConversationModelId",
                principalTable: "Conversations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_UserModel_UserModelId",
                table: "UserModel",
                column: "UserModelId",
                principalTable: "UserModel",
                principalColumn: "Id");
        }
    }
}
