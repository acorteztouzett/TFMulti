﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrabajoFinalMulti.Data;

#nullable disable

namespace TrabajoFinalMulti.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231018205630_TablaAnuncios")]
    partial class TablaAnuncios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("TrabajoFinalMulti.Models.Docente", b =>
                {
                    b.Property<int>("Docente_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Docente_Id"));

                    b.Property<string>("Docente_Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Docente_Correo")
                        .IsRequired()
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

                    b.Property<string>("Estudiante_Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estudiante_Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Estudiante_Id");

                    b.ToTable("Estudiante");
                });
#pragma warning restore 612, 618
        }
    }
}