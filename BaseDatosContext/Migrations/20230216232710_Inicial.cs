using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseDatosContext.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEquipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caratula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deporte = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Torneos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modalidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetsMax = table.Column<int>(type: "int", nullable: false),
                    PuntajeMax = table.Column<int>(type: "int", nullable: false),
                    Deporte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hasta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosContadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosContadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipoJugador",
                columns: table => new
                {
                    EquiposId = table.Column<int>(type: "int", nullable: false),
                    JugadoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoJugador", x => new { x.EquiposId, x.JugadoresId });
                    table.ForeignKey(
                        name: "FK_EquipoJugador_Equipos_EquiposId",
                        column: x => x.EquiposId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoJugador_Jugadores_JugadoresId",
                        column: x => x.JugadoresId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipoTorneo",
                columns: table => new
                {
                    InscripcionesId = table.Column<int>(type: "int", nullable: false),
                    TorneosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTorneo", x => new { x.InscripcionesId, x.TorneosId });
                    table.ForeignKey(
                        name: "FK_EquipoTorneo_Equipos_InscripcionesId",
                        column: x => x.InscripcionesId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoTorneo_Torneos_TorneosId",
                        column: x => x.TorneosId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<int>(type: "int", nullable: false),
                    VisitanteId = table.Column<int>(type: "int", nullable: false),
                    MarcadorLocal = table.Column<int>(type: "int", nullable: false),
                    MarcadorVisitante = table.Column<int>(type: "int", nullable: false),
                    SetsGanadosLocal = table.Column<int>(type: "int", nullable: false),
                    SetsGanadosVisitante = table.Column<int>(type: "int", nullable: false),
                    SetActual = table.Column<int>(type: "int", nullable: false),
                    PartidoSiguienteGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TorneoId = table.Column<int>(type: "int", nullable: true),
                    NombreCancha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Posición = table.Column<int>(type: "int", nullable: false),
                    HistorialPartido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Ronda = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RondaDescanso = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidos_Equipos_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidos_Equipos_VisitanteId",
                        column: x => x.VisitanteId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Partidos_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipoJugador_JugadoresId",
                table: "EquipoJugador",
                column: "JugadoresId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTorneo_TorneosId",
                table: "EquipoTorneo",
                column: "TorneosId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_LocalId",
                table: "Partidos",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId",
                table: "Partidos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_VisitanteId",
                table: "Partidos",
                column: "VisitanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipoJugador");

            migrationBuilder.DropTable(
                name: "EquipoTorneo");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "UsuariosContadores");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Torneos");
        }
    }
}
