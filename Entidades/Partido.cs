using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Partido
    {
        public int Id { get; set; }
        [ForeignKey("Local")]
        public int? LocalId { get; set; }
        public virtual Equipo? Local { get; set; } = new();

        [ForeignKey("Visitante")]
        public int? VisitanteId { get; set; }
        public virtual Equipo? Visitante { get; set; } = new();
        public int MarcadorLocal { get; set; }
        public int MarcadorVisitante { get; set; }
        public int SetsGanadosLocal { get; set; }
        public int SetsGanadosVisitante { get; set; }
        public int SetActual { get; set; }
        public int PuntajeLocal { get; set; }
        public int PuntajeVisitante { get; set; }
        public Guid PartidoSiguienteGuid { get; set; }
        public int TorneoId { get; set; } = new();
        public Torneo Torneo { get; set; } = new();
        public string NombreCancha { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int Posición { get; set; }
        public string HistorialPartido { get; set; } = string.Empty;
        public string Lugar { get; set; } = string.Empty;


        public int Orden { get; set; }
        public int Ronda { get; set; }
        public Guid Guid { get; set; }
        public bool RondaDescanso { get; set; }
    }
}
