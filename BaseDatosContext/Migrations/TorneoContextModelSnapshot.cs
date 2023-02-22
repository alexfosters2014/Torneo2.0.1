﻿// <auto-generated />
using System;
using BaseDatosContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BaseDatosContext.Migrations
{
    [DbContext(typeof(TorneoContext))]
    partial class TorneoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entidades.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Caratula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Deporte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEquipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("Entidades.Jugador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cedula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jugadores");
                });

            modelBuilder.Entity("Entidades.Partido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HistorialPartido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocalId")
                        .HasColumnType("int");

                    b.Property<string>("Lugar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MarcadorLocal")
                        .HasColumnType("int");

                    b.Property<int>("MarcadorVisitante")
                        .HasColumnType("int");

                    b.Property<string>("NombreCancha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<Guid>("PartidoSiguienteGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Posición")
                        .HasColumnType("int");

                    b.Property<int>("PuntajeLocal")
                        .HasColumnType("int");

                    b.Property<int>("PuntajeVisitante")
                        .HasColumnType("int");

                    b.Property<int>("Ronda")
                        .HasColumnType("int");

                    b.Property<bool>("RondaDescanso")
                        .HasColumnType("bit");

                    b.Property<int>("SetActual")
                        .HasColumnType("int");

                    b.Property<int>("SetsGanadosLocal")
                        .HasColumnType("int");

                    b.Property<int>("SetsGanadosVisitante")
                        .HasColumnType("int");

                    b.Property<int?>("TorneoId")
                        .HasColumnType("int");

                    b.Property<int>("VisitanteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocalId");

                    b.HasIndex("TorneoId");

                    b.HasIndex("VisitanteId");

                    b.ToTable("Partidos");
                });

            modelBuilder.Entity("Entidades.Torneo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Deporte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Desde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Hasta")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modalidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PuntajeMax")
                        .HasColumnType("int");

                    b.Property<int>("SetsMax")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Torneos");
                });

            modelBuilder.Entity("Entidades.UsuarioContador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UsuariosContadores");
                });

            modelBuilder.Entity("EquipoJugador", b =>
                {
                    b.Property<int>("EquiposId")
                        .HasColumnType("int");

                    b.Property<int>("JugadoresId")
                        .HasColumnType("int");

                    b.HasKey("EquiposId", "JugadoresId");

                    b.HasIndex("JugadoresId");

                    b.ToTable("EquipoJugador");
                });

            modelBuilder.Entity("EquipoTorneo", b =>
                {
                    b.Property<int>("InscripcionesId")
                        .HasColumnType("int");

                    b.Property<int>("TorneosId")
                        .HasColumnType("int");

                    b.HasKey("InscripcionesId", "TorneosId");

                    b.HasIndex("TorneosId");

                    b.ToTable("EquipoTorneo");
                });

            modelBuilder.Entity("Entidades.Partido", b =>
                {
                    b.HasOne("Entidades.Equipo", "Local")
                        .WithMany()
                        .HasForeignKey("LocalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidades.Torneo", "Torneo")
                        .WithMany("Fixture")
                        .HasForeignKey("TorneoId");

                    b.HasOne("Entidades.Equipo", "Visitante")
                        .WithMany()
                        .HasForeignKey("VisitanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Local");

                    b.Navigation("Torneo");

                    b.Navigation("Visitante");
                });

            modelBuilder.Entity("EquipoJugador", b =>
                {
                    b.HasOne("Entidades.Equipo", null)
                        .WithMany()
                        .HasForeignKey("EquiposId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidades.Jugador", null)
                        .WithMany()
                        .HasForeignKey("JugadoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EquipoTorneo", b =>
                {
                    b.HasOne("Entidades.Equipo", null)
                        .WithMany()
                        .HasForeignKey("InscripcionesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidades.Torneo", null)
                        .WithMany()
                        .HasForeignKey("TorneosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.Torneo", b =>
                {
                    b.Navigation("Fixture");
                });
#pragma warning restore 612, 618
        }
    }
}
