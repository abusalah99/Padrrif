using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padrrif.Migrations
{
    /// <inheritdoc />
    public partial class FixDamgeIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Comittee_ComiteeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Comittee");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Damage",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Damage",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Damage",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Damage_DocumentId",
                table: "Damage",
                column: "DocumentId",
                unique: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "Damage",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);


            migrationBuilder.CreateTable(
                name: "Comitee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comitee", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Comitee_ComiteeId",
                table: "User",
                column: "ComiteeId",
                principalTable: "Comitee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Comitee_ComiteeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Comitee");

            migrationBuilder.DropIndex(
                name: "IX_Damage_DocumentId",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Damage");

            migrationBuilder.UpdateData(
                table: "Damage",
                keyColumn: "DocumentNumber",
                keyValue: null,
                column: "DocumentNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Damage",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comittee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comittee", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Comittee_ComiteeId",
                table: "User",
                column: "ComiteeId",
                principalTable: "Comittee",
                principalColumn: "Id");
        }
    }
}
