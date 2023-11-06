﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrabajoFinalMulti.Data;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TrabajoFinalMulti.Models.Administrador", b =>
                {
                    b.Property<int>("Admin_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Admin_Id"));

                    b.Property<string>("Admin_Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Admin_Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Admin_Id");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.AnuncioInformativo", b =>
                {
                    b.Property<int>("Anuncio_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Anuncio_Id"));

                    b.Property<string>("Anuncio_URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Anuncio_Id");

                    b.ToTable("AnuncioInformativo");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Apoderado", b =>
                {
                    b.Property<int>("Apoderado_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Apoderado_Id"));

                    b.Property<string>("Apoderado_Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apoderado_Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apoderado_DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apoderado_Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apoderado_Parentesco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Apoderado_Id");

                    b.ToTable("Apoderado");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Asesoria", b =>
                {
                    b.Property<int>("Asesoria_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Asesoria_Id"));

                    b.Property<int>("Curso_Id")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tema")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Asesoria_Id");

                    b.HasIndex("Curso_Id");

                    b.ToTable("Asesorias");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Aula", b =>
                {
                    b.Property<int>("Aula_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Aula_Id"));

                    b.Property<int>("Aula_Aforo")
                        .HasColumnType("int");

                    b.Property<string>("Aula_Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Aula_Grado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Aula_NivelEducativo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Aula_Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Periodo_Id")
                        .HasColumnType("int");

                    b.HasKey("Aula_Id");

                    b.HasIndex("Periodo_Id");

                    b.ToTable("Aula");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Curso", b =>
                {
                    b.Property<int>("Curso_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Curso_Id"));

                    b.Property<int>("Aula_Id")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad_Horas")
                        .HasColumnType("int");

                    b.Property<string>("Curso_Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Docente_Id")
                        .HasColumnType("int");

                    b.HasKey("Curso_Id");

                    b.HasIndex("Aula_Id");

                    b.HasIndex("Docente_Id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Docente", b =>
                {
                    b.Property<int>("Docente_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Docente_Id"));

                    b.Property<string>("Docente_Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Docente_FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Docente_Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Docente_Id");

                    b.ToTable("Docente");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Estudiante", b =>
                {
                    b.Property<int>("Estudiante_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Estudiante_Id"));

                    b.Property<int?>("Apoderado_Id")
                        .HasColumnType("int");

                    b.Property<string>("Estudiante_Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Estudiante_FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estudiante_Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Estudiante_Id");

                    b.HasIndex("Apoderado_Id")
                        .IsUnique()
                        .HasFilter("[Apoderado_Id] IS NOT NULL");

                    b.ToTable("Estudiante");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.EstudiantePorSesion", b =>
                {
                    b.Property<int>("EstudiantesPorSesion_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstudiantesPorSesion_Id"));

                    b.Property<bool>("Asistio")
                        .HasColumnType("bit");

                    b.Property<int>("Estudiante_Id")
                        .HasColumnType("int");

                    b.Property<int>("Sesion_Id")
                        .HasColumnType("int");

                    b.HasKey("EstudiantesPorSesion_Id");

                    b.HasIndex("Estudiante_Id");

                    b.HasIndex("Sesion_Id");

                    b.ToTable("EstudiantesPorSesions");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.EstudiantesPorCurso", b =>
                {
                    b.Property<int>("Estudiante_Id")
                        .HasColumnType("int");

                    b.Property<int>("Curso_Id")
                        .HasColumnType("int");

                    b.Property<int>("EstudiantesPorCurso_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstudiantesPorCurso_Id"));

                    b.HasKey("Estudiante_Id", "Curso_Id");

                    b.HasIndex("Curso_Id");

                    b.ToTable("EstudiantesPorCursos");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Evaluacion", b =>
                {
                    b.Property<int>("Evaluacion_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Evaluacion_Id"));

                    b.Property<int>("Curso_Id")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Evaluacion_Id");

                    b.HasIndex("Curso_Id");

                    b.ToTable("Evaluaciones");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.EvaluacionPorEstudiante", b =>
                {
                    b.Property<int>("EvaluacionPorEstudiante_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvaluacionPorEstudiante_Id"));

                    b.Property<int>("Estudiante_Id")
                        .HasColumnType("int");

                    b.Property<int>("Evaluacion_Id")
                        .HasColumnType("int");

                    b.Property<float>("Nota")
                        .HasColumnType("real");

                    b.HasKey("EvaluacionPorEstudiante_Id");

                    b.HasIndex("Estudiante_Id");

                    b.HasIndex("Evaluacion_Id");

                    b.ToTable("EvaluacionPorEstudiantes");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Horario", b =>
                {
                    b.Property<int>("Horario_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Horario_Id"));

                    b.Property<string>("Dia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hora_Fin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hora_Inicio")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Horario_Id");

                    b.ToTable("Horario");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Periodo", b =>
                {
                    b.Property<int>("Periodo_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Periodo_Id"));

                    b.Property<string>("Periodo_Año")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Periodo_FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Periodo_FechaInicio")
                        .HasColumnType("datetime2");

                    b.HasKey("Periodo_Id");

                    b.ToTable("Periodo");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Sesion", b =>
                {
                    b.Property<int>("Sesiones_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sesiones_Id"));

                    b.Property<int>("Curso_Id")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tema")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sesiones_Id");

                    b.HasIndex("Curso_Id");

                    b.ToTable("Sesiones");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Asesoria", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("Curso_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Aula", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Periodo", "Periodo")
                        .WithMany("Aula")
                        .HasForeignKey("Periodo_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Periodo");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Curso", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Aula", "Aula")
                        .WithMany("Curso")
                        .HasForeignKey("Aula_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrabajoFinalMulti.Models.Docente", "Docente")
                        .WithMany("Curso")
                        .HasForeignKey("Docente_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aula");

                    b.Navigation("Docente");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Estudiante", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Apoderado", "Apoderado")
                        .WithOne("Estudiante")
                        .HasForeignKey("TrabajoFinalMulti.Models.Estudiante", "Apoderado_Id");

                    b.Navigation("Apoderado");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.EstudiantePorSesion", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Estudiante", "Estudiante")
                        .WithMany("EstudiantePorSesions")
                        .HasForeignKey("Estudiante_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrabajoFinalMulti.Models.Sesion", "Sesion")
                        .WithMany("EstudiantePorSesions")
                        .HasForeignKey("Sesion_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Sesion");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.EstudiantesPorCurso", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Curso", "Curso")
                        .WithMany("EstudiantesPorCursos")
                        .HasForeignKey("Curso_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrabajoFinalMulti.Models.Estudiante", "Estudiante")
                        .WithMany("EstudiantesPorCursos")
                        .HasForeignKey("Estudiante_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Evaluacion", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Curso", "Curso")
                        .WithMany("Evaluacions")
                        .HasForeignKey("Curso_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.EvaluacionPorEstudiante", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Estudiante", "Estudiante")
                        .WithMany("EvaluacionPorEstudiantes")
                        .HasForeignKey("Estudiante_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrabajoFinalMulti.Models.Evaluacion", "Evaluacion")
                        .WithMany("EvaluacionPorEstudiantes")
                        .HasForeignKey("Evaluacion_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Evaluacion");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Sesion", b =>
                {
                    b.HasOne("TrabajoFinalMulti.Models.Curso", "Curso")
                        .WithMany("Sesiones")
                        .HasForeignKey("Curso_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Apoderado", b =>
                {
                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Aula", b =>
                {
                    b.Navigation("Curso");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Curso", b =>
                {
                    b.Navigation("EstudiantesPorCursos");

                    b.Navigation("Evaluacions");

                    b.Navigation("Sesiones");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Docente", b =>
                {
                    b.Navigation("Curso");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Estudiante", b =>
                {
                    b.Navigation("EstudiantePorSesions");

                    b.Navigation("EstudiantesPorCursos");

                    b.Navigation("EvaluacionPorEstudiantes");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Evaluacion", b =>
                {
                    b.Navigation("EvaluacionPorEstudiantes");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Periodo", b =>
                {
                    b.Navigation("Aula");
                });

            modelBuilder.Entity("TrabajoFinalMulti.Models.Sesion", b =>
                {
                    b.Navigation("EstudiantePorSesions");
                });
#pragma warning restore 612, 618
        }
    }
}
