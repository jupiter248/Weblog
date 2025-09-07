using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeletedViewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLists_AspNetUsers_appUserId",
                table: "FavoriteLists");

            migrationBuilder.DropTable(
                name: "ViewContents");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "FavoriteLists");

            migrationBuilder.RenameColumn(
                name: "appUserId",
                table: "FavoriteLists",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteLists_appUserId",
                table: "FavoriteLists",
                newName: "IX_FavoriteLists_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Podcasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLists_AspNetUsers_AppUserId",
                table: "FavoriteLists",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLists_AspNetUsers_AppUserId",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "FavoriteLists",
                newName: "appUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteLists_AppUserId",
                table: "FavoriteLists",
                newName: "IX_FavoriteLists_appUserId");

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "FavoriteLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ViewContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    PodcastId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ViewedOn = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewContents_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ViewContents_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ViewContents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ViewContents_Podcasts_PodcastId",
                        column: x => x.PodcastId,
                        principalTable: "Podcasts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ViewContents_AppUserId",
                table: "ViewContents",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewContents_ArticleId",
                table: "ViewContents",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewContents_EventId",
                table: "ViewContents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewContents_PodcastId",
                table: "ViewContents",
                column: "PodcastId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLists_AspNetUsers_appUserId",
                table: "FavoriteLists",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
