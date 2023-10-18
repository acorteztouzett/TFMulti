using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    /// <inheritdoc />
    public partial class TablaCurso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Curso_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Curso_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Curso_Id);
                    table.ForeignKey(
                        name: "FK_Curso_Docente_Docente_Id",
                        column: x => x.Docente_Id,
                        principalTable: "Docente",
                        principalColumn: "Docente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Docente_Id",
                table: "Curso",
                column: "Docente_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curso");
        }
    }
}
