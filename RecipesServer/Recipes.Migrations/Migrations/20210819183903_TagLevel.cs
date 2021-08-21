using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Migrations
{
    public partial class TagLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_selected",
                table: "tag");

            migrationBuilder.AddColumn<int>(
                name: "tag_level",
                table: "tag",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tag_level",
                table: "tag");

            migrationBuilder.AddColumn<bool>(
                name: "is_selected",
                table: "tag",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
