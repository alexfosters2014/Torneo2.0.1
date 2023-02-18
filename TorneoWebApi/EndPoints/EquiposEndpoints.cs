using Entidades;
using Negocio;

namespace TorneoWebApi.EndPoints
{
    public static class EquiposEndpoints
    {
        public static void MapEquiposEndpoints(this WebApplication app)
        {
            app.MapGet("/Equipo/Get/{id}", GetEquipo);
            app.MapPost("/Equipo/Nuevo", NuevoEquipo);
            app.MapGet("/Equipo/Get/Find/{nombre}", GetsEquiposPorNombre);
        }

        public static async Task<IResult> GetEquipo(int id, EquiposService equipoService)
        {
            try
            {
                var resultado = await equipoService.GetEquipo(id);
                if (resultado == null) return Results.BadRequest("El equipo no está ingresado en el sistema");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> GetsEquiposPorNombre(string nombre, EquiposService equipoService)
        {
            try
            {
                var resultado = await equipoService.GetsEquiposPorNombre(nombre);
                if (resultado == null) return Results.BadRequest("El equipo no está ingresado en el sistema");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> NuevoEquipo(Equipo equipo, EquiposService equipoService)
        {
            try
            {
                var resultado = await equipoService.NuevoEquipo(equipo);
                if (resultado == null) return Results.BadRequest("No se pudo procesar su solicitud");

                if (resultado <= 0) return Results.BadRequest("No se pudo ingresar al equipo");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
