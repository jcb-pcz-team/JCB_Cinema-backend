using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCB_Cinema.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDialCodeToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DialCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DialCode",
                table: "AspNetUsers");
        }
    }
}
