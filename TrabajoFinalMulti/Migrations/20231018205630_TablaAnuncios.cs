using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    /// <inheritdoc />
    public partial class TablaAnuncios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnuncioInformativo",
                columns: table => new
                {
                    Anuncio_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anuncio_URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioInformativo", x => x.Anuncio_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnuncioInformativo");
        }
    }
}
