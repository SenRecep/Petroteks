using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Petroteks.Dal.Migrations
{
    public partial class AddProductMl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ML_Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateUserid = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUserid = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    WebSiteid = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductLanguageKeyCode = table.Column<string>(nullable: true),
                    AlternateProductId = table.Column<int>(nullable: false),
                    AlternateProductLanguageKeyCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ML_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_ML_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ML_Products_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ML_Products_ProductId",
                table: "ML_Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ML_Products_WebSiteid",
                table: "ML_Products",
                column: "WebSiteid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ML_Products");
        }
    }
}
