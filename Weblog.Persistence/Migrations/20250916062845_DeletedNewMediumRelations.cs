using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeletedNewMediumRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Articles_EntityId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Contributors_EntityId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Events_EntityId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Podcasts_EntityId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_EntityId",
                table: "Media");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Media_EntityId",
                table: "Media",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Articles_EntityId",
                table: "Media",
                column: "EntityId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Contributors_EntityId",
                table: "Media",
                column: "EntityId",
                principalTable: "Contributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Events_EntityId",
                table: "Media",
                column: "EntityId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Podcasts_EntityId",
                table: "Media",
                column: "EntityId",
                principalTable: "Podcasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
