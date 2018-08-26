using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contexto.Migrations
{
    public partial class arreglandoLosGeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Serie");

            migrationBuilder.CreateTable(
                name: "GeneroSerie",
                columns: table => new
                {
                    GeneroSerieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GeneroId = table.Column<int>(nullable: false),
                    GeneroId1 = table.Column<int>(nullable: true),
                    SerieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroSerie", x => x.GeneroSerieId);
                    table.ForeignKey(
                        name: "FK_GeneroSerie_Usuario_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroSerie_Genero_GeneroId1",
                        column: x => x.GeneroId1,
                        principalTable: "Genero",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneroSerie_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneroSerie_GeneroId",
                table: "GeneroSerie",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroSerie_GeneroId1",
                table: "GeneroSerie",
                column: "GeneroId1");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroSerie_SerieId",
                table: "GeneroSerie",
                column: "SerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneroSerie");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Serie",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
