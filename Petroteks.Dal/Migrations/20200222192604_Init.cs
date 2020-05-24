using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Petroteks.Dal.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateUserid = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUserid = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    TagName = table.Column<string>(nullable: true),
                    Role = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateUserid = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUserid = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    BaseUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
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
                    EmailAddress = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_Emails_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
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
                    Name = table.Column<string>(nullable: true),
                    KeyCode = table.Column<string>(nullable: true),
                    IconCode = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.id);
                    table.ForeignKey(
                        name: "FK_Languages_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsObjects",
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
                    Name = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaTags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsObjects", x => x.id);
                    table.ForeignKey(
                        name: "FK_AboutUsObjects_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AboutUsObjects_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
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
                    Keywords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaTags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Blogs_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
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
                    Parentid = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_Categories_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPages",
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
                    Name = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaTags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPages", x => x.id);
                    table.ForeignKey(
                        name: "FK_DynamicPages_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPages_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainPages",
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
                    Name = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaTags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Slider = table.Column<string>(nullable: true),
                    TopContent = table.Column<string>(nullable: true),
                    BottomContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainPages", x => x.id);
                    table.ForeignKey(
                        name: "FK_MainPages_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainPages_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivacyPolicyObjects",
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
                    Name = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaTags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacyPolicyObjects", x => x.id);
                    table.ForeignKey(
                        name: "FK_PrivacyPolicyObjects_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivacyPolicyObjects_Websites_WebSiteid",
                        column: x => x.WebSiteid,
                        principalTable: "Websites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateUserid = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUserid = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Categoryid = table.Column<int>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true),
                    SupTitle = table.Column<string>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaTags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Languageid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Languages_Languageid",
                        column: x => x.Languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsObjects_Languageid",
                table: "AboutUsObjects",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsObjects_WebSiteid",
                table: "AboutUsObjects",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Languageid",
                table: "Blogs",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_WebSiteid",
                table: "Blogs",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Languageid",
                table: "Categories",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_WebSiteid",
                table: "Categories",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPages_Languageid",
                table: "DynamicPages",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPages_WebSiteid",
                table: "DynamicPages",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_WebSiteid",
                table: "Emails",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_WebSiteid",
                table: "Languages",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_MainPages_Languageid",
                table: "MainPages",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_MainPages_WebSiteid",
                table: "MainPages",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_PrivacyPolicyObjects_Languageid",
                table: "PrivacyPolicyObjects",
                column: "Languageid");

            migrationBuilder.CreateIndex(
                name: "IX_PrivacyPolicyObjects_WebSiteid",
                table: "PrivacyPolicyObjects",
                column: "WebSiteid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Categoryid",
                table: "Products",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Languageid",
                table: "Products",
                column: "Languageid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsObjects");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "DynamicPages");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "MainPages");

            migrationBuilder.DropTable(
                name: "PrivacyPolicyObjects");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Websites");
        }
    }
}
