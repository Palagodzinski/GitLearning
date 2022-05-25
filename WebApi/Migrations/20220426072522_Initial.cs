using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Usr_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usr_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usr_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usr_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usr_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usr_Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Usr_ID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Bks_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bks_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bks_Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usr_UsrID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Bks_ID);
                    table.ForeignKey(
                        name: "FK_Books_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Usr_ID");
                });

            migrationBuilder.CreateTable(
                name: "DelayedBooks",
                columns: table => new
                {
                    Db_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserUsr_ID = table.Column<int>(type: "int", nullable: false),
                    BookBks_ID = table.Column<int>(type: "int", nullable: false),
                    Db_DelayDaysCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayedBooks", x => x.Db_Id);
                    table.ForeignKey(
                        name: "FK_DelayedBooks_Books_BookBks_ID",
                        column: x => x.BookBks_ID,
                        principalTable: "Books",
                        principalColumn: "Bks_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DelayedBooks_Users_UserUsr_ID",
                        column: x => x.UserUsr_ID,
                        principalTable: "Users",
                        principalColumn: "Usr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserID",
                table: "Books",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DelayedBooks_BookBks_ID",
                table: "DelayedBooks",
                column: "BookBks_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DelayedBooks_UserUsr_ID",
                table: "DelayedBooks",
                column: "UserUsr_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelayedBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
