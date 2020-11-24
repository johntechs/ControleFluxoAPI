using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFluxoAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsOcupada = table.Column<bool>(type: "bit", nullable: false),
                    EstoqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagas_Estoques_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VagaId = table.Column<int>(type: "int", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "DateTime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estoques",
                columns: new[] { "Id", "nome" },
                values: new object[] { 100, "Estoque Principal" });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Fornecedor de Peças" });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Fornecedor de Frutas" });

            migrationBuilder.InsertData(
                table: "Vagas",
                columns: new[] { "Id", "EstoqueId", "IsOcupada" },
                values: new object[] { 1, 100, false });

            migrationBuilder.InsertData(
                table: "Vagas",
                columns: new[] { "Id", "EstoqueId", "IsOcupada" },
                values: new object[] { 2, 100, false });

            migrationBuilder.InsertData(
                table: "Vagas",
                columns: new[] { "Id", "EstoqueId", "IsOcupada" },
                values: new object[] { 3, 100, false });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_FornecedorId",
                table: "Agendamentos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_VagaId",
                table: "Agendamentos",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_EstoqueId",
                table: "Vagas",
                column: "EstoqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropTable(
                name: "Estoques");
        }
    }
}
