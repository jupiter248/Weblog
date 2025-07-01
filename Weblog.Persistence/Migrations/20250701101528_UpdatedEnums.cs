using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentTypeId",
                table: "Media",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "ParentType",
                table: "Media",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "CategoryParentType",
                table: "Categories",
                newName: "EntityType");

            migrationBuilder.AddColumn<int>(
                name: "ContributorType",
                table: "Podcasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContributorId",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Contributors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ContributorId",
                table: "Media",
                column: "ContributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Contributors_ContributorId",
                table: "Media",
                column: "ContributorId",
                principalTable: "Contributors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Contributors_ContributorId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_ContributorId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ContributorType",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "ContributorId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Contributors");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "Media",
                newName: "ParentTypeId");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "Media",
                newName: "ParentType");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "Categories",
                newName: "CategoryParentType");
        }
    }
}
