using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Migrations
{
    public partial class AuthorForRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "author_id",
                table: "recipe",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_recipe_author_id",
                table: "recipe",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipe_users_author_id",
                table: "recipe",
                column: "author_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipe_users_author_id",
                table: "recipe");

            migrationBuilder.DropIndex(
                name: "ix_recipe_author_id",
                table: "recipe");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "recipe");
        }
    }
}
