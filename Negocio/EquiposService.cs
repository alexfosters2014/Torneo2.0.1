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

namespace Negocio
{
    public class EquiposService
    {
        private readonly TorneoContext _db;
        public EquiposService(TorneoContext _db)
        {
            this._db = _db;
        }

        public async Task<Equipo> GetEquipo(int id)
        {
            return await _db.Equipos.FindAsync(id);
        }

        public async Task<List<Equipo>> GetsEquiposPorNombre(string nombre)
        {
            return await _db.Equipos.Where(w => w.NombreEquipo.Contains(nombre.ToUpper().Trim())).ToListAsync();
        }

        public async Task<int> NuevoEquipo(Equipo equipo)
        {
            try
            {
                string mensajeError = "";
                equipo.NombreEquipo = equipo.NombreEquipo.ToUpper().Trim();

                ValidadorEquipo validacion = new(_db);
                ValidationResult result = validacion.Validate(equipo);
                if (!result.IsValid)
                {
                    result.Errors.ForEach(f => mensajeError += f.ErrorMessage);
                    throw new Exception(mensajeError);
                }

                equipo.Jugadores.ForEach(jugador =>  _db.Entry(jugador).State = EntityState.Unchanged);

                var nuevo = await _db.AddAsync(equipo);
                await _db.SaveChangesAsync();

                return nuevo.Entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
