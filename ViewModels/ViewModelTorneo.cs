using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ViewModelTorneo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImagenRef { get; set; } = string.Empty;
        public List<EquipoVM> Inscripciones { get; set; } = new();
        public List<PartidoVM> Fixture { get; set; } = new();
        public string Modalidad { get; set; } = string.Empty;
        public int SetsMax { get; set; }
        public int PuntajeMax { get; set; }
        public int PuntajeMaxDefinitorio { get; set; }
        public string Deporte { get; set; } = string.Empty;
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }

    }
    public class EquipoVM
    {
        public int Id { get; set; }
        public string NombreEquipo { get; set; } = string.Empty;
        public string Caratula { get; set; } = string.Empty;
        public string Deporte { get; set; } = string.Empty;
    }

    public class PartidoVM
    {
        public int Id { get; set; }
        [ForeignKey("Local")]
        public int? LocalId { get; set; }
        public EquipoVM? Local { get; set; } = new();

        [ForeignKey("Visitante")]
        public int? VisitanteId { get; set; }
        public EquipoVM? Visitante { get; set; } = new();
        public int MarcadorLocal { get; set; }
        public int MarcadorVisitante { get; set; }
        public int SetsGanadosLocal { get; set; }
        public int SetsGanadosVisitante { get; set; }
        public int SetActual { get; set; } = 1;
        public int PuntajeLocal { get; set; }
        public int PuntajeVisitante { get; set; }
        public Guid PartidoSiguienteGuid { get; set; }
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
