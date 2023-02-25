namespace Entidades
{
    public class Equipo
    {
        public int Id { get; set; }
        public string NombreEquipo { get; set; } = string.Empty;
        public string Caratula { get; set; } = string.Empty;
        public virtual List<Jugador> Jugadores { get; set; } = new();
        public string Deporte { get; set; } = string.Empty;
        public virtual List<Torneo> Torneos { get; set; } = new();
    }
}
