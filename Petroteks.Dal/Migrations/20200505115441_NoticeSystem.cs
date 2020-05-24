using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Petroteks.Dal.Migrations
{
    public partial class NoticeSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UI_Notices",
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
                    Content = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UI_Notices", x => x.id);
                    table.ForeignKey(
                        name: "FK_UI_Notices_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UI_Notices_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UI_Notices_Languageid",
                table: "UI_Notices",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_UI_Notices_WebSiteid",
                table: "UI_Notices",
                column: "WebSiteid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UI_Notices");
        }
    }
}
