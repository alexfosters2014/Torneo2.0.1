using Entidades;

namespace ViewModels
{
    public class ViewModelInscripcion
    {
        public int TorneoId { get; set; }
        public string NombreTorneo { get; set; }
        public Equipo EquipoAInscribir { get; set; }
    }
}