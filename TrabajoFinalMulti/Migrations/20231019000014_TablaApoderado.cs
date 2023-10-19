using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    /// <inheritdoc />
    public partial class TablaApoderado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Apoderado_Id",
                table: "Estudiante",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Apoderado",
                columns: table => new
                {
                    Apoderado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apoderado_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apoderado_Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apoderado_DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apoderado_Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apoderado_Correo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoderado", x => x.Apoderado_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_Apoderado_Id",
                table: "Estudiante",
                column: "Apoderado_Id",
                unique: true,
                filter: "[Apoderado_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiante_Apoderado_Apoderado_Id",
                table: "Estudiante",
                column: "Apoderado_Id",
                principalTable: "Apoderado",
                principalColumn: "Apoderado_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiante_Apoderado_Apoderado_Id",
                table: "Estudiante");

            migrationBuilder.DropTable(
                name: "Apoderado");

            migrationBuilder.DropIndex(
                name: "IX_Estudiante_Apoderado_Id",
                table: "Estudiante");

            migrationBuilder.DropColumn(
                name: "Apoderado_Id",
                table: "Estudiante");
        }
    }
}
