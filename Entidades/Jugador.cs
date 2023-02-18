namespace Entidades
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Equipo> Equipos { get; set; } = new();
    }
}
