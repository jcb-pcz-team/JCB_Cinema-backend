﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCB_Cinema.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId",
                table: "BookingTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId",
                table: "BookingTickets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId",
                table: "BookingTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTickets_AspNetUsers_AppUserId",
                table: "BookingTickets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
