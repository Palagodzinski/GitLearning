using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Api.Migrations
{
    public partial class ChangedUserFieldsNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Hyg",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Usr_Password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Usr_Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Usr_LastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Usr_Email");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Usr_ID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Books",
                newName: "Bks_UserID");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Bks_Title");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "Bks_Author");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Books",
                newName: "Bks_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserID",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_Bks_UserID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Usr_Password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Usr_Name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Usr_LastName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Usr_Email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Usr_ID",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Bks_UserID",
                table: "Books",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Bks_Title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Bks_Author",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "Bks_ID",
                table: "Books",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Bks_UserID",
                table: "Books",
                newName: "IX_Books_UserID");

            migrationBuilder.AddColumn<string>(
                name: "Hyg",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserID",
                table: "Books",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
