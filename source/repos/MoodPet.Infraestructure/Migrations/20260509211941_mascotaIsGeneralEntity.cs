using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPet.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class mascotaIsGeneralEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreaAt",
                table: "Mascotas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Mascotas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreaAt",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Mascotas");
        }
    }
}
