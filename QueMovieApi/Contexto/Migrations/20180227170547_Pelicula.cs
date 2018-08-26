using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contexto.Migrations
{
    public partial class Pelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pelicula");
        }
    }
}
