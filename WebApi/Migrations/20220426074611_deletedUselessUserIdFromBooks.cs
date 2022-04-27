using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Api.Migrations
{
    public partial class deletedUselessUserIdFromBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usr_UsrID",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Usr_UsrID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
