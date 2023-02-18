namespace Entidades
{
    public class Equipo
    {
        public int Id { get; set; }
        public string NombreEquipo { get; set; }
        public string Caratula { get; set; }
        public virtual List<Jugador> Jugadores { get; set; } = new();
        public string Deporte { get; set; }
        public virtual List<Torneo> Torneos { get; set; }
    }
}
