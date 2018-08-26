using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contexto.Migrations
{
    public partial class Comentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioCapitulo",
                columns: table => new
                {
                    UsuarioCapituloId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CapituloID = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    Editado = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCapitulo", x => x.UsuarioCapituloId);
                    table.ForeignKey(
                        name: "FK_UsuarioCapitulo_Capitulo_CapituloID",
                        column: x => x.CapituloID,
                        principalTable: "Capitulo",
                        principalColumn: "CapituloID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCapitulo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCapitulo_CapituloID",
                table: "UsuarioCapitulo",
                column: "CapituloID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCapitulo_UsuarioId",
                table: "UsuarioCapitulo",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioCapitulo");
        }
    }
}
