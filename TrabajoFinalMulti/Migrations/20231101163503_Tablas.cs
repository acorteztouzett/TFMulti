using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    /// <inheritdoc />
    public partial class Tablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin_Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin_Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Admin_Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Docente",
                columns: table => new
                {
                    Docente_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Docente_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Docente_DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente_Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Docente_Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docente", x => x.Docente_Id);
                });

            migrationBuilder.CreateTable(
                name: "Periodo",
                columns: table => new
                {
                    Periodo_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Periodo_Año = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Periodo_FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Periodo_FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodo", x => x.Periodo_Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Estudiante_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estudiante_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estudiante_DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estudiante_Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estudiante_Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apoderado_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Estudiante_Id);
                    table.ForeignKey(
                        name: "FK_Estudiante_Apoderado_Apoderado_Id",
                        column: x => x.Apoderado_Id,
                        principalTable: "Apoderado",
                        principalColumn: "Apoderado_Id");
                });

            migrationBuilder.CreateTable(
                name: "Aula",
                columns: table => new
                {
                    Aula_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aula_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aula_Aforo = table.Column<int>(type: "int", nullable: false),
                    Aula_NivelEducativo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aula_Grado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aula_Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Periodo_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aula", x => x.Aula_Id);
                    table.ForeignKey(
                        name: "FK_Aula_Periodo_Periodo_Id",
                        column: x => x.Periodo_Id,
                        principalTable: "Periodo",
                        principalColumn: "Periodo_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Curso_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Curso_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad_Horas = table.Column<int>(type: "int", nullable: false),
                    Docente_Id = table.Column<int>(type: "int", nullable: false),
                    Aula_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Curso_Id);
                    table.ForeignKey(
                        name: "FK_Curso_Aula_Aula_Id",
                        column: x => x.Aula_Id,
                        principalTable: "Aula",
                        principalColumn: "Aula_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curso_Docente_Docente_Id",
                        column: x => x.Docente_Id,
                        principalTable: "Docente",
                        principalColumn: "Docente_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstudiantesPorCursos",
                columns: table => new
                {
                    Curso_Id = table.Column<int>(type: "int", nullable: false),
                    Estudiante_Id = table.Column<int>(type: "int", nullable: false),
                    EstudiantesPorCurso_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudiantesPorCursos", x => new { x.Estudiante_Id, x.Curso_Id });
                    table.ForeignKey(
                        name: "FK_EstudiantesPorCursos_Curso_Curso_Id",
                        column: x => x.Curso_Id,
                        principalTable: "Curso",
                        principalColumn: "Curso_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudiantesPorCursos_Estudiante_Estudiante_Id",
                        column: x => x.Estudiante_Id,
                        principalTable: "Estudiante",
                        principalColumn: "Estudiante_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluaciones",
                columns: table => new
                {
                    Evaluacion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Curso_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluaciones", x => x.Evaluacion_Id);
                    table.ForeignKey(
                        name: "FK_Evaluaciones_Curso_Curso_Id",
                        column: x => x.Curso_Id,
                        principalTable: "Curso",
                        principalColumn: "Curso_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    Sesiones_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Curso_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.Sesiones_Id);
                    table.ForeignKey(
                        name: "FK_Sesiones_Curso_Curso_Id",
                        column: x => x.Curso_Id,
                        principalTable: "Curso",
                        principalColumn: "Curso_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionPorEstudiantes",
                columns: table => new
                {
                    EvaluacionPorEstudiante_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evaluacion_Id = table.Column<int>(type: "int", nullable: false),
                    Estudiante_Id = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionPorEstudiantes", x => x.EvaluacionPorEstudiante_Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionPorEstudiantes_Estudiante_Estudiante_Id",
                        column: x => x.Estudiante_Id,
                        principalTable: "Estudiante",
                        principalColumn: "Estudiante_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionPorEstudiantes_Evaluaciones_Evaluacion_Id",
                        column: x => x.Evaluacion_Id,
                        principalTable: "Evaluaciones",
                        principalColumn: "Evaluacion_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstudiantesPorSesions",
                columns: table => new
                {
                    EstudiantesPorSesion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sesion_Id = table.Column<int>(type: "int", nullable: false),
                    Estudiante_Id = table.Column<int>(type: "int", nullable: false),
                    Asistio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudiantesPorSesions", x => x.EstudiantesPorSesion_Id);
                    table.ForeignKey(
                        name: "FK_EstudiantesPorSesions_Estudiante_Estudiante_Id",
                        column: x => x.Estudiante_Id,
                        principalTable: "Estudiante",
                        principalColumn: "Estudiante_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudiantesPorSesions_Sesiones_Sesion_Id",
                        column: x => x.Sesion_Id,
                        principalTable: "Sesiones",
                        principalColumn: "Sesiones_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aula_Periodo_Id",
                table: "Aula",
                column: "Periodo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Aula_Id",
                table: "Curso",
                column: "Aula_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Docente_Id",
                table: "Curso",
                column: "Docente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_Apoderado_Id",
                table: "Estudiante",
                column: "Apoderado_Id",
                unique: true,
                filter: "[Apoderado_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesPorCursos_Curso_Id",
                table: "EstudiantesPorCursos",
                column: "Curso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesPorSesions_Estudiante_Id",
                table: "EstudiantesPorSesions",
                column: "Estudiante_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EstudiantesPorSesions_Sesion_Id",
                table: "EstudiantesPorSesions",
                column: "Sesion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluaciones_Curso_Id",
                table: "Evaluaciones",
                column: "Curso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionPorEstudiantes_Estudiante_Id",
                table: "EvaluacionPorEstudiantes",
                column: "Estudiante_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionPorEstudiantes_Evaluacion_Id",
                table: "EvaluacionPorEstudiantes",
                column: "Evaluacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sesiones_Curso_Id",
                table: "Sesiones",
                column: "Curso_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "AnuncioInformativo");

            migrationBuilder.DropTable(
                name: "EstudiantesPorCursos");

            migrationBuilder.DropTable(
                name: "EstudiantesPorSesions");

            migrationBuilder.DropTable(
                name: "EvaluacionPorEstudiantes");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Evaluaciones");

            migrationBuilder.DropTable(
                name: "Apoderado");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Aula");

            migrationBuilder.DropTable(
                name: "Docente");

            migrationBuilder.DropTable(
                name: "Periodo");
        }
    }
}
