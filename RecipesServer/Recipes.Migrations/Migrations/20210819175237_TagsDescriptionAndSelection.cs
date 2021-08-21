using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Migrations
{
    public partial class TagsDescriptionAndSelection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tag_ids",
                table: "recipe");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "tag",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_selected",
                table: "tag",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "is_selected",
                table: "tag");

            migrationBuilder.AddColumn<int[]>(
                name: "tag_ids",
                table: "recipe",
                type: "integer[]",
                nullable: true);
        }
    }
}
