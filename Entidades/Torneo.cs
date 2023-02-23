namespace Entidades
{
    public class Torneo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImagenRef { get; set; }
        public virtual List<Equipo> Inscripciones { get; set; } = new();
        public virtual List<Partido> Fixture { get; set; } = new();
        public string Modalidad { get; set; }
        public int SetsMax { get; set; }
        public int PuntajeMax { get; set; }
        public int PuntajeMaxDefinitorio { get; set; }
        public string Deporte { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }
}
