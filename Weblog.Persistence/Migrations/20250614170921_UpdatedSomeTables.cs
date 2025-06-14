using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DisplayedAt",
                table: "Podcasts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Articles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayedAt",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Articles");
        }
    }
}
