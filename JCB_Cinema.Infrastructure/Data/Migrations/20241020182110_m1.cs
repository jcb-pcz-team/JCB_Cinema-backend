using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCB_Cinema.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId",
                table: "BookingTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_CinemaHalls_HallId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_HallId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_BookingTickets_AppUserId",
                table: "BookingTickets");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "MoviesProjection");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "MoviesProjection");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "CinemaHalls");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "CinemaHalls");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "BookingTickets");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "BookingTickets");

            migrationBuilder.RenameColumn(
                name: "HallId",
                table: "Seats",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookingTickets",
                newName: "ModifierId");

            migrationBuilder.AddColumn<int>(
                name: "CinemaHallId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifierId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifierId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "MoviesProjection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifierId",
                table: "MoviesProjection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifierId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "CinemaHalls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifierId",
                table: "CinemaHalls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "BookingTickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "BookingTickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "BookingTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CinemaHallId",
                table: "Seats",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTickets_AppUserId1",
                table: "BookingTickets",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId1",
                table: "BookingTickets",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_CinemaHalls_CinemaHallId",
                table: "Seats",
                column: "CinemaHallId",
                principalTable: "CinemaHalls",
                principalColumn: "CinemaHallId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId1",
                table: "BookingTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_CinemaHalls_CinemaHallId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_CinemaHallId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_BookingTickets_AppUserId1",
                table: "BookingTickets");

            migrationBuilder.DropColumn(
                name: "CinemaHallId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MoviesProjection");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "MoviesProjection");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "CinemaHalls");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "CinemaHalls");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "BookingTickets");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "BookingTickets");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Seats",
                newName: "HallId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "BookingTickets",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Seats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Seats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "MoviesProjection",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "MoviesProjection",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "CinemaHalls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "CinemaHalls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "BookingTickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "BookingTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "BookingTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallId",
                table: "Seats",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTickets_AppUserId",
                table: "BookingTickets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId",
                table: "BookingTickets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_CinemaHalls_HallId",
                table: "Seats",
                column: "HallId",
                principalTable: "CinemaHalls",
                principalColumn: "CinemaHallId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
