using Microsoft.EntityFrameworkCore.Migrations;

namespace Petroteks.Dal.Migrations
{
    public partial class AddProductMlx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ML_Products_AlternateProductId",
                table: "ML_Products",
                column: "AlternateProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ML_Products_Products_AlternateProductId",
                table: "ML_Products",
                column: "AlternateProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ML_Products_Products_AlternateProductId",
                table: "ML_Products");

            migrationBuilder.DropIndex(
                name: "IX_ML_Products_AlternateProductId",
                table: "ML_Products");
        }
    }
}
