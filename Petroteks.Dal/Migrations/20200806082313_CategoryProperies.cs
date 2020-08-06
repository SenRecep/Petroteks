using Microsoft.EntityFrameworkCore.Migrations;

namespace Petroteks.Dal.Migrations
{
    public partial class CategoryProperies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTags",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MetaTags",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Categories");
        }
    }
}
