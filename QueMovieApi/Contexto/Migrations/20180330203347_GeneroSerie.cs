using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contexto.Migrations
{
    public partial class GeneroSerie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneroSerie_Usuario_GeneroId",
                table: "GeneroSerie");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneroSerie_Genero_GeneroId1",
                table: "GeneroSerie");

            migrationBuilder.DropIndex(
                name: "IX_GeneroSerie_GeneroId1",
                table: "GeneroSerie");

            migrationBuilder.DropColumn(
                name: "GeneroId1",
                table: "GeneroSerie");

            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "Usuario",
                newName: "Sexo");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroSerie_Genero_GeneroId",
                table: "GeneroSerie",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "GeneroId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneroSerie_Genero_GeneroId",
                table: "GeneroSerie");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Usuario",
                newName: "Genero");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId1",
                table: "GeneroSerie",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneroSerie_GeneroId1",
                table: "GeneroSerie",
                column: "GeneroId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroSerie_Usuario_GeneroId",
                table: "GeneroSerie",
                column: "GeneroId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroSerie_Genero_GeneroId1",
                table: "GeneroSerie",
                column: "GeneroId1",
                principalTable: "Genero",
                principalColumn: "GeneroId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
