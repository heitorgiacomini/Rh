using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Funcionario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Funcionario_nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Funcionario_datadenascimento = table.Column<DateTime>(type: "datetime", nullable: true),
                    Funcionario_salario = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Funcionario_id);
                });

            migrationBuilder.CreateTable(
                name: "Filho",
                columns: table => new
                {
                    Filho_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filho_nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Filho_datadenascimento = table.Column<DateTime>(type: "datetime", nullable: true),
                    Filho_FuncionarioPai = table.Column<int>(type: "int", nullable: true),
                    Filho_FuncionarioMae = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filho", x => x.Filho_id);
                    table.ForeignKey(
                        name: "FK_Filho_FuncionarioMae",
                        column: x => x.Filho_FuncionarioMae,
                        principalTable: "Funcionario",
                        principalColumn: "Funcionario_id");
                    table.ForeignKey(
                        name: "FK_Filho_FuncionarioPai",
                        column: x => x.Filho_FuncionarioPai,
                        principalTable: "Funcionario",
                        principalColumn: "Funcionario_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filho_Filho_FuncionarioMae",
                table: "Filho",
                column: "Filho_FuncionarioMae");

            migrationBuilder.CreateIndex(
                name: "IX_Filho_Filho_FuncionarioPai",
                table: "Filho",
                column: "Filho_FuncionarioPai");

            migrationBuilder.CreateIndex(
                name: "nomeesalarioIsUNIQUE",
                table: "Funcionario",
                columns: new[] { "Funcionario_nome", "Funcionario_salario" },
                unique: true,
                filter: "[Funcionario_nome] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filho");

            migrationBuilder.DropTable(
                name: "Funcionario");
        }
    }
}
