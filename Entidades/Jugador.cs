namespace Entidades
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public List<Equipo> Equipos { get; set; } = new();
    }
}
