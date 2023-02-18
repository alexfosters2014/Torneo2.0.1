using Entidades;
using Negocio;

namespace TorneoWebApi.EndPoints
{
    public static class JugadoresEndpoints
    {
        public static void MapJugadoresEndpoints(this WebApplication app)
        {
            app.MapGet("/Jugador/Get/{cedula}", GetJugador);
            app.MapPost("/Jugador/Nuevo", NuevoJugador);
        }

        public static async Task<IResult> GetJugador(JugadoresService jugadorService, string cedula)
        {
            try
            {
                var resultado = await jugadorService.GetJugador(cedula);
                if (resultado == null) return Results.BadRequest("El jugador no está ingresado en el sistema");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> NuevoJugador(JugadoresService jugadorService, Jugador jugador)
        {
            try
            {
                var resultado = await jugadorService.NuevoJugador(jugador);
                if (resultado == null) return Results.BadRequest("No se pudo procesar su solicitud");

                if (resultado <= 0) return Results.BadRequest("No se pudo ingresar al jugador");

                return Results.Ok(resultado);

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }


    }
}
