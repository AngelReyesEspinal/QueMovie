using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contexto.Migrations
{
    public partial class SeCambioPeliculasPorSeriesYSusCapitulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Usuario",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Serie",
                columns: table => new
                {
                    SerieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anio = table.Column<int>(nullable: false),
                    Duracion = table.Column<string>(maxLength: 20, nullable: false),
                    Genero = table.Column<string>(maxLength: 30, nullable: false),
                    Imagen = table.Column<string>(maxLength: 50, nullable: false),
                    Productora = table.Column<string>(maxLength: 50, nullable: false),
                    Sinopsis = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(maxLength: 80, nullable: false),
                    Trailer = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serie", x => x.SerieId);
                });

            migrationBuilder.CreateTable(
                name: "Capitulo",
                columns: table => new
                {
                    CapituloID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DireccionDelCapitulo = table.Column<string>(maxLength: 80, nullable: false),
                    NombreDelCapitulo = table.Column<string>(maxLength: 80, nullable: false),
                    NumeroCapitulo = table.Column<int>(nullable: false),
                    SerieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitulo", x => x.CapituloID);
                    table.ForeignKey(
                        name: "FK_Capitulo_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "SerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capitulo_SerieId",
                table: "Capitulo",
                column: "SerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capitulo");

            migrationBuilder.DropTable(
                name: "Serie");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anio = table.Column<int>(nullable: false),
                    Duracion = table.Column<string>(maxLength: 20, nullable: false),
                    Genero = table.Column<string>(maxLength: 30, nullable: false),
                    Imagen = table.Column<string>(maxLength: 50, nullable: false),
                    PeliculaCompleta = table.Column<string>(maxLength: 100, nullable: false),
                    Productora = table.Column<string>(maxLength: 50, nullable: false),
                    Sinopsis = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(maxLength: 80, nullable: false),
                    Trailer = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.PeliculaId);
                });
        }
    }
}
