using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Petroteks.Dal.Migrations
{
    public partial class StaticContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UI_Contacts",
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
                    Languageid = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UI_Contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_UI_Contacts_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UI_Contacts_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UI_Footers",
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
                    Languageid = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UI_Footers", x => x.id);
                    table.ForeignKey(
                        name: "FK_UI_Footers_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UI_Footers_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UI_Navbars",
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
                    Languageid = table.Column<int>(nullable: true),
                    Home = table.Column<string>(nullable: true),
                    Products = table.Column<string>(nullable: true),
                    AboutUs = table.Column<string>(nullable: true),
                    PetroBlog = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Pages = table.Column<string>(nullable: true),
                    Languages = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UI_Navbars", x => x.id);
                    table.ForeignKey(
                        name: "FK_UI_Navbars_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UI_Navbars_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UI_Contacts_Languageid",
                table: "UI_Contacts",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_UI_Contacts_WebSiteid",
                table: "UI_Contacts",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_UI_Footers_Languageid",
                table: "UI_Footers",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_UI_Footers_WebSiteid",
                table: "UI_Footers",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_UI_Navbars_Languageid",
                table: "UI_Navbars",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_UI_Navbars_WebSiteid",
                table: "UI_Navbars",
                column: "WebSiteid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UI_Contacts");

            migrationBuilder.DropTable(
                name: "UI_Footers");

            migrationBuilder.DropTable(
                name: "UI_Navbars");
        }
    }
}
