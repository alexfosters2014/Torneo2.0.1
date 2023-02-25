namespace Entidades
{
    public class Torneo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImagenRef { get; set; } = string.Empty;
        public virtual List<Equipo> Inscripciones { get; set; } = new();
        public virtual List<Partido> Fixture { get; set; } = new();
        public string Modalidad { get; set; } = string.Empty;
        public int SetsMax { get; set; }
        public int PuntajeMax { get; set; }
        public int PuntajeMaxDefinitorio { get; set; }
        public string Deporte { get; set; } = string.Empty;
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }
}
