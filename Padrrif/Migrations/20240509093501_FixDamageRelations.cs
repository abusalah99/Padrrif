using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padrrif.Migrations
{
    /// <inheritdoc />
    public partial class FixDamageRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentsPaths",
                table: "Damage",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Damage",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmerId",
                table: "Damage",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "LocationPaths",
                table: "Damage",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Damage_EmployeeId",
                table: "Damage",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Damage_User_EmployeeId",
                table: "Damage",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damage_User_EmployeeId",
                table: "Damage");

            migrationBuilder.DropIndex(
                name: "IX_Damage_EmployeeId",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "DocumentsPaths",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "LocationPaths",
                table: "Damage");
        }
    }
}
