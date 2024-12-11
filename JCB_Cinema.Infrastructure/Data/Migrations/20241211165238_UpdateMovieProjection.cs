using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCB_Cinema.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price_AmountInCents",
                table: "MoviesProjection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Price_Currency",
                table: "MoviesProjection",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_AmountInCents",
                table: "MoviesProjection");

            migrationBuilder.DropColumn(
                name: "Price_Currency",
                table: "MoviesProjection");
        }
    }
}
