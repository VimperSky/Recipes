using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Migrations
{
    public partial class TagRecipesManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tag_recipes_recipe_id",
                table: "tag");

            migrationBuilder.DropIndex(
                name: "ix_tag_recipe_id",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "recipe_id",
                table: "tag");

            migrationBuilder.CreateTable(
                name: "recipe_tag",
                columns: table => new
                {
                    recipes_id = table.Column<int>(type: "integer", nullable: false),
                    tags_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipe_tag", x => new { x.recipes_id, x.tags_value });
                    table.ForeignKey(
                        name: "fk_recipe_tag_recipes_recipes_id",
                        column: x => x.recipes_id,
                        principalTable: "recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_recipe_tag_tag_tags_value",
                        column: x => x.tags_value,
                        principalTable: "tag",
                        principalColumn: "value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_recipe_tag_tags_value",
                table: "recipe_tag",
                column: "tags_value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe_tag");

            migrationBuilder.AddColumn<int>(
                name: "recipe_id",
                table: "tag",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_tag_recipe_id",
                table: "tag",
                column: "recipe_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tag_recipes_recipe_id",
                table: "tag",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
