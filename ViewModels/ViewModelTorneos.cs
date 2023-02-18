using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ViewModelTorneos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImagenRef { get; set; }
        public List<EquipoVM> Inscripciones { get; set; } = new();
        public List<PartidoVM> Fixture { get; set; } = new();
        public string Modalidad { get; set; }
        public int SetsMax { get; set; }
        public int PuntajeMax { get; set; }
        public string Deporte { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }

    }
    public class EquipoVM
    {
        public int Id { get; set; }
        public string NombreEquipo { get; set; }
        public string Caratula { get; set; }
        public string Deporte { get; set; }
    }

    public class PartidoVM
    {
        public int Id { get; set; }
        [ForeignKey("Local")]
        public int LocalId { get; set; }
        public EquipoVM Local { get; set; } = new();

        [ForeignKey("Visitante")]
        public int VisitanteId { get; set; }
        public EquipoVM Visitante { get; set; } = new();
        public int MarcadorLocal { get; set; }
        public int MarcadorVisitante { get; set; }
        public int SetsGanadosLocal { get; set; }
        public int SetsGanadosVisitante { get; set; }
        public int SetActual { get; set; }
        public Guid PartidoSiguienteGuid { get; set; }
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
