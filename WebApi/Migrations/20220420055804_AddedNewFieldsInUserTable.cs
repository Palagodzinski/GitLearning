using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Api.Migrations
{
    public partial class AddedNewFieldsInUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_Bks_UserID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Bks_UserID",
                table: "Books",
                newName: "UsersUsr_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Bks_UserID",
                table: "Books",
                newName: "IX_Books_UsersUsr_ID");

            migrationBuilder.AddColumn<DateTime>(
                name: "Usr_Created",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UsersUsr_ID",
                table: "Books",
                column: "UsersUsr_ID",
                principalTable: "Users",
                principalColumn: "Usr_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UsersUsr_ID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Usr_Created",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UsersUsr_ID",
                table: "Books",
                newName: "Bks_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UsersUsr_ID",
                table: "Books",
                newName: "IX_Books_Bks_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_Bks_UserID",
                table: "Books",
                column: "Bks_UserID",
                principalTable: "Users",
                principalColumn: "Usr_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
