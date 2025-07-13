using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFavoriteTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "Podcasts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "FavoritePodcasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "FavoriteEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "FavoriteArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    appUserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteLists_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Podcasts_FavoriteListId",
                table: "Podcasts",
                column: "FavoriteListId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_FavoriteListId",
                table: "Events",
                column: "FavoriteListId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FavoriteListId",
                table: "Articles",
                column: "FavoriteListId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLists_appUserId",
                table: "FavoriteLists",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_FavoriteLists_FavoriteListId",
                table: "Articles",
                column: "FavoriteListId",
                principalTable: "FavoriteLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FavoriteLists_FavoriteListId",
                table: "Events",
                column: "FavoriteListId",
                principalTable: "FavoriteLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Podcasts_FavoriteLists_FavoriteListId",
                table: "Podcasts",
                column: "FavoriteListId",
                principalTable: "FavoriteLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_FavoriteLists_FavoriteListId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_FavoriteLists_FavoriteListId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Podcasts_FavoriteLists_FavoriteListId",
                table: "Podcasts");

            migrationBuilder.DropTable(
                name: "FavoriteLists");

            migrationBuilder.DropIndex(
                name: "IX_Podcasts_FavoriteListId",
                table: "Podcasts");

            migrationBuilder.DropIndex(
                name: "IX_Events_FavoriteListId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Articles_FavoriteListId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "FavoritePodcasts");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "FavoriteEvents");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "FavoriteArticles");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "Articles");
        }
    }
}
