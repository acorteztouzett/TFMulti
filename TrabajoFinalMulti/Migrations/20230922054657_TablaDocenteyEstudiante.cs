using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    /// <inheritdoc />
    public partial class TablaDocenteyEstudiante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Docente",
                columns: table => new
                {
                    Docente_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Docente_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docente", x => x.Docente_Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Estudiante_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estudiante_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Estudiante_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Docente");

            migrationBuilder.DropTable(
                name: "Estudiante");
        }
    }
}
