using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedGuestTakingPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TakingParts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "TakingParts",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GuestFamily",
                table: "TakingParts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "TakingParts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GuestPhone",
                table: "TakingParts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ParticipantType",
                table: "TakingParts",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TakingParts_AppUserId1",
                table: "TakingParts",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TakingParts_AspNetUsers_AppUserId1",
                table: "TakingParts",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakingParts_AspNetUsers_AppUserId1",
                table: "TakingParts");

            migrationBuilder.DropIndex(
                name: "IX_TakingParts_AppUserId1",
                table: "TakingParts");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "TakingParts");

            migrationBuilder.DropColumn(
                name: "GuestFamily",
                table: "TakingParts");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "TakingParts");

            migrationBuilder.DropColumn(
                name: "GuestPhone",
                table: "TakingParts");

            migrationBuilder.DropColumn(
                name: "ParticipantType",
                table: "TakingParts");

            migrationBuilder.UpdateData(
                table: "TakingParts",
                keyColumn: "UserId",
                keyValue: null,
                column: "UserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TakingParts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
