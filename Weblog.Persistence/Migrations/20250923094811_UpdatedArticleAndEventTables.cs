using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedArticleAndEventTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboveTitle",
                table: "Events",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BelowTitle",
                table: "Events",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AboveTitle",
                table: "Articles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BelowTitle",
                table: "Articles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboveTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BelowTitle",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AboveTitle",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "BelowTitle",
                table: "Articles");
        }
    }
}
