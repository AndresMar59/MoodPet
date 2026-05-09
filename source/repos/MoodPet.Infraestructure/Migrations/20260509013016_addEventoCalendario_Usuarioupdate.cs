using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPet.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class addEventoCalendario_Usuarioupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuario_UsuarioId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Mascotas",
                newName: "Usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_Mascotas_UsuarioId",
                table: "Mascotas",
                newName: "IX_Mascotas_Usuarioid");

            migrationBuilder.AddColumn<Guid>(
                name: "Usuarioid",
                table: "TareasDiarias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Usuarioid",
                table: "Mascotas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Mascotas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreaAt",
                table: "EventosCalendario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "EventosCalendario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EventosCalendario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Usuarioid",
                table: "EventosCalendario",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TareasDiarias_Usuarioid",
                table: "TareasDiarias",
                column: "Usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioId",
                table: "Mascotas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosCalendario_Usuarioid",
                table: "EventosCalendario",
                column: "Usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_EventosCalendario_Usuario_Usuarioid",
                table: "EventosCalendario",
                column: "Usuarioid",
                principalTable: "Usuario",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuario_UsuarioId",
                table: "Mascotas",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuario_Usuarioid",
                table: "Mascotas",
                column: "Usuarioid",
                principalTable: "Usuario",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TareasDiarias_Usuario_Usuarioid",
                table: "TareasDiarias",
                column: "Usuarioid",
                principalTable: "Usuario",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventosCalendario_Usuario_Usuarioid",
                table: "EventosCalendario");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuario_UsuarioId",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuario_Usuarioid",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_TareasDiarias_Usuario_Usuarioid",
                table: "TareasDiarias");

            migrationBuilder.DropIndex(
                name: "IX_TareasDiarias_Usuarioid",
                table: "TareasDiarias");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_EventosCalendario_Usuarioid",
                table: "EventosCalendario");

            migrationBuilder.DropColumn(
                name: "Usuarioid",
                table: "TareasDiarias");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "CreaAt",
                table: "EventosCalendario");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "EventosCalendario");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EventosCalendario");

            migrationBuilder.DropColumn(
                name: "Usuarioid",
                table: "EventosCalendario");

            migrationBuilder.RenameColumn(
                name: "Usuarioid",
                table: "Mascotas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Mascotas_Usuarioid",
                table: "Mascotas",
                newName: "IX_Mascotas_UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Mascotas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuario_UsuarioId",
                table: "Mascotas",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
