using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.WebApi.Migrations
{
    public partial class renamepasswordhashtopassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "user",
                newName: "password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "user",
                newName: "password_hash");
        }
    }
}
