using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace alten_test.DataAccessLayer.Migrations
{
    public partial class UpdateReservationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Reservations",
                type: "varchar(85)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(85)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_ApplicationUserId",
                table: "Reservations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_ApplicationUserId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Reservations",
                type: "varchar(85)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(85)");

            
        }
    }
}
