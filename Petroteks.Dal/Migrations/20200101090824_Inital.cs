using Microsoft.EntityFrameworkCore.Migrations;

namespace Petroteks.Dal.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderObjects_MainPages_MainPageid",
                table: "SliderObjects");

            migrationBuilder.DropIndex(
                name: "IX_SliderObjects_MainPageid",
                table: "SliderObjects");

            migrationBuilder.DropColumn(
                name: "MainPageid",
                table: "SliderObjects");

            migrationBuilder.AddColumn<string>(
                name: "Slider",
                table: "MainPages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slider",
                table: "MainPages");

            migrationBuilder.AddColumn<int>(
                name: "MainPageid",
                table: "SliderObjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SliderObjects_MainPageid",
                table: "SliderObjects",
                column: "MainPageid");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderObjects_MainPages_MainPageid",
                table: "SliderObjects",
                column: "MainPageid",
                principalTable: "MainPages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
