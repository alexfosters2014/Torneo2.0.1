using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Partido
    {
        public int Id { get; set; }
        [ForeignKey("Local")]
        public int LocalId { get; set; }
        public Equipo Local { get; set; } = new();

        [ForeignKey("Visitante")]
        public int VisitanteId { get; set; }
        public Equipo Visitante { get; set; } = new();
        public int MarcadorLocal { get; set; }
        public int MarcadorVisitante { get; set; }
        public int SetsGanadosLocal { get; set; }
        public int SetsGanadosVisitante { get; set; }
        public int SetActual { get; set; }
        public Guid PartidoSiguienteGuid { get; set; }
        public Torneo Torneo { get; set; } = new();
        public string NombreCancha { get; set; }
        public DateTime Fecha { get; set; }
        public int Posición { get; set; }
        public string HistorialPartido { get; set; }
        public string Lugar { get; set; }


        public int Orden { get; set; }
        public int Ronda { get; set; }
        public Guid Guid { get; set; }
        public bool RondaDescanso { get; set; }
    }
}
