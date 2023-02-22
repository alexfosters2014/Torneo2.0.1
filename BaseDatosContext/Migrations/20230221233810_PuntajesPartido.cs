using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseDatosContext.Migrations
{
    /// <inheritdoc />
    public partial class PuntajesPartido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PuntajeLocal",
                table: "Partidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntajeVisitante",
                table: "Partidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntajeLocal",
                table: "Partidos");

            migrationBuilder.DropColumn(
                name: "PuntajeVisitante",
                table: "Partidos");
        }
    }
}
