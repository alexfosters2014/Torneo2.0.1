using BaseDatosContext;
using Entidades;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Negocio.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Negocio
{
    public class JugadoresService
    {
        private readonly TorneoContext _db;
        public JugadoresService(TorneoContext db)
        {
            _db = db;
        }

        public async Task<Jugador> GetJugador(string cedula)
        {
            return await _db.Jugadores.SingleOrDefaultAsync(s => s.Cedula == cedula);
        } 

        public async Task<int> NuevoJugador(Jugador jugador)
        {
            try
            {
                string mensajeError = "";

                jugador.Nombres = jugador.Nombres.ToUpper().Trim();
                jugador.Apellidos = jugador.Apellidos.ToUpper().Trim();
                jugador.Cedula = jugador.Cedula.Trim();

                ValidadorJugador validacion = new(_db);
                ValidationResult result = validacion.Validate(jugador);
                if (!result.IsValid) {
                    result.Errors.ForEach(f => mensajeError += f.ErrorMessage);
                    throw new Exception(mensajeError);
                }

                var nuevoJugador = await _db.AddAsync(jugador);
                await _db.SaveChangesAsync();

                return nuevoJugador.Entity.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
