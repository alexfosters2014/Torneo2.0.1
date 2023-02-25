using Entidades;
using Negocio;
using ViewModels;

namespace TorneoWebApi.EndPoints
{
    public static class TorneosEndpoints
    {
        public static void MapTorneosEndpoints(this WebApplication app)
        {
            app.MapGet("/Torneo/Get/Inscripcion/{deporte}", GetTorneosPorDeporteInscripcion);
            app.MapGet("/Torneo/Get/Nombre/{nombre}", GetTorneosPorNombre);
            app.MapGet("/Torneo/Get/Vigentes", GetTorneosVigentes);
            app.MapPost("/Torneo/Inscripcion", InscribirEquipoATorneo);
            app.MapPost("/Torneo/Crear", CrearTorneo);
            app.MapPost("/Torneo/Guardar/Fixture", GuardarFixture);
            app.MapPost("/Torneo/Actualizar/Partido", ActualizarPartido);
        }

        public static async Task<IResult> GetTorneosPorDeporteInscripcion(TorneoService torneoService, string deporte)
        {
            try
            {
                var resultado = await torneoService.GetTorneosDeporteInscripcion(deporte);
                if (resultado == null) return Results.BadRequest("El torneo no está ingresado en el sistema");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> GetTorneosPorNombre(TorneoService torneoService, string nombre)
        {
            try
            {
                var resultado = await torneoService.GetTorneosVigentes();
                if (resultado == null) return Results.BadRequest("No hay torneos a mostrar");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> GetTorneosVigentes(TorneoService torneoService)
        {
            try
            {
                var resultado = await torneoService.GetTorneosVigentes();
                if (resultado == null) return Results.BadRequest("El torneo no está ingresado en el sistema");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> InscribirEquipoATorneo(TorneoService torneoService, ViewModelInscripcion viewModelInscripcion)
        {
            try
            {
                var resultado = await torneoService.InscribirEquipo(viewModelInscripcion);
                if (resultado == null) return Results.BadRequest("El torneo no está ingresado en el sistema");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> CrearTorneo(TorneoService torneoService, Torneo torneo)
        {
            try
            {
                var resultado = await torneoService.CrearTorneo(torneo);
                if (resultado == null) return Results.BadRequest("El torneo no se ha podido crear");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> GuardarFixture(TorneoService torneoService, ViewModelFixture viewModelFixture)
        {
            try
            {
                var resultado = await torneoService.GuardarFixtureCompleto(viewModelFixture.TorneoId, viewModelFixture.Fixture);
                if (resultado == false) return Results.BadRequest("El fixture no se ha podido guardar");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> ActualizarPartido(TorneoService torneoService, PartidoVM partidoVM)
        {
            try
            {
                var resultado = await torneoService.ActualizarPartido(partidoVM);
                if (resultado == false) return Results.BadRequest("El partido no se ha podido actualizar");

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

    }
}
