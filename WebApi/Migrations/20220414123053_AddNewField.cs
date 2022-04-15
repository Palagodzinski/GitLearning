using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Api.Migrations
{
    public partial class AddNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hyg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hyg",
                table: "Users");
        }
    }
}
