using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class fixForm2m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Users_ActivityId",
                table: "ActivityAttendees");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "ActivityAttendees",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAttendees_AppUserId",
                table: "ActivityAttendees",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_Users_AppUserId",
                table: "ActivityAttendees",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Users_AppUserId",
                table: "ActivityAttendees");

            migrationBuilder.DropIndex(
                name: "IX_ActivityAttendees_AppUserId",
                table: "ActivityAttendees");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "ActivityAttendees",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_Users_ActivityId",
                table: "ActivityAttendees",
                column: "ActivityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
