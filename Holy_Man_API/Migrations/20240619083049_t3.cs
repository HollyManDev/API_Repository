using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holy_Man_API.Migrations
{
    /// <inheritdoc />
    public partial class t3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_Users_UserId",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Messages_MessageId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserModel");

            migrationBuilder.AddColumn<int>(
                name: "ConversationModelId",
                table: "UserModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "UserModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_ConversationModelId",
                table: "UserModel",
                column: "ConversationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_UserModelId",
                table: "UserModel",
                column: "UserModelId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_UserModel_ConversationModelId",
                table: "UserModel");

            migrationBuilder.DropIndex(
                name: "IX_UserModel_UserModelId",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "ConversationModelId",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "UserModel");

            migrationBuilder.RenameTable(
                name: "UserModel",
                newName: "Users");

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
                    ConversationId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserModelId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserModelId",
                table: "Messages",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_Users_UserId",
                table: "ConversationParticipants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Messages_MessageId",
                table: "Documents",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }
    }
}
