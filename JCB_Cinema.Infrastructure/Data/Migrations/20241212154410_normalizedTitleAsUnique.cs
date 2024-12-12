using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCB_Cinema.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class normalizedTitleAsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NormalizedTitle",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_NormalizedTitle",
                table: "Movies",
                column: "NormalizedTitle",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_NormalizedTitle",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedTitle",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
