using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeilaoService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leiloes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrecoReserva = table.Column<int>(type: "integer", nullable: false),
                    Vendedor = table.Column<string>(type: "text", nullable: true),
                    Vencedor = table.Column<string>(type: "text", nullable: true),
                    QuantidadeVendida = table.Column<int>(type: "integer", nullable: true),
                    LanceAtualMaisAlto = table.Column<int>(type: "integer", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leiloes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fabrica = table.Column<string>(type: "text", nullable: true),
                    Modelo = table.Column<string>(type: "text", nullable: true),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Cor = table.Column<string>(type: "text", nullable: true),
                    Quilometragem = table.Column<int>(type: "integer", nullable: false),
                    URLImagem = table.Column<string>(type: "text", nullable: true),
                    LeilaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Leiloes_LeilaoId",
                        column: x => x.LeilaoId,
                        principalTable: "Leiloes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_LeilaoId",
                table: "Items",
                column: "LeilaoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Leiloes");
        }
    }
}
