using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab4Final.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibroId",
                table: "Socios",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDevolucion",
                table: "Prestamos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_LibroId",
                table: "Socios",
                column: "LibroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Socios_Libros_LibroId",
                table: "Socios",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socios_Libros_LibroId",
                table: "Socios");

            migrationBuilder.DropIndex(
                name: "IX_Socios_LibroId",
                table: "Socios");

            migrationBuilder.DropColumn(
                name: "LibroId",
                table: "Socios");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDevolucion",
                table: "Prestamos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
