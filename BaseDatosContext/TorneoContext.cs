using Entidades;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosContext
{
    public class TorneoContext : DbContext
    {
        public TorneoContext(DbContextOptions options) : base(options)
        {
           // this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<UsuarioContador> UsuariosContadores { get; set; }
    }
}