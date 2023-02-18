using BaseDatosContext;
using Entidades;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Azure;
using Negocio.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Negocio
{
    public class TorneoService
    {
        private readonly TorneoContext _db;
        public TorneoService(TorneoContext db)
        {
            _db = db;
        }

        public async Task<List<ViewModelTorneos>> GetTorneosDeporteInscripcion(string deporte)
        {
            return await _db.Torneos.Where(t => t.Deporte == deporte)
                              .Select(s => new ViewModelTorneos()
                              {
                                    Id = s.Id,
                                    Nombre= s.Nombre,
                              })
                              .ToListAsync();


        }

        public async Task<List<ViewModelTorneos>> GetTorneosVigentes()
        {
            var torneos = await _db.Torneos.Where(w => w.Desde >= DateTime.Today)
                                     .Select(s => new ViewModelTorneos()
                                     {
                                         Id = s.Id,
                                         Nombre = s.Nombre,
                                         ImagenRef = s.ImagenRef,
                                         Modalidad = s.Modalidad,
                                         SetsMax = s.SetsMax,
                                         PuntajeMax = s.PuntajeMax,
                                         Deporte = s.Deporte,
                                         Desde = s.Desde,
                                         Hasta = s.Hasta,
                                         Fixture = new List<PartidoVM>(),
                                         Inscripciones = s.Inscripciones.Select(inscripcion => new EquipoVM()
                                         {
                                             Id= inscripcion.Id,
                                             NombreEquipo = inscripcion.NombreEquipo,
                                             Caratula = inscripcion.Caratula,
                                             Deporte= inscripcion.Deporte
                                         }).ToList()
                                     }).ToListAsync();

            return torneos;
        }

        public async Task<List<Torneo>> GetTorneosNombre(string nombre)
        {
            return _db.Torneos.Include(i => i.Inscripciones)
                              .Include(i => i.Fixture)
                              .Where(t => t.Nombre.Contains(nombre.ToUpper())).ToList();
        }

        public async Task<string> InscribirEquipo(ViewModelInscripcion viewModelInscripcion)
        {
            try
            {
                string mensajeError = "";

                ValidadorInscripcion validacion = new(_db);
                ValidationResult result = validacion.Validate(viewModelInscripcion);
                if (!result.IsValid)
                {
                    result.Errors.ForEach(f => mensajeError += f.ErrorMessage);
                    throw new Exception(mensajeError);
                }

                var torneoDB = await _db.Torneos.SingleOrDefaultAsync(s => s.Id == viewModelInscripcion.TorneoId);
                                                
                if (torneoDB == null) throw new Exception("no existe el torneo seleccionado");

                torneoDB.Inscripciones.Add(viewModelInscripcion.EquipoAInscribir);
                await _db.SaveChangesAsync();
                return "Felicidades, se ha inscripto satisfactoriamente al torneo " + torneoDB.Nombre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> CrearTorneo(Torneo torneo)
        {
            try
            {
                string mensajeError = "";

                torneo.Nombre = torneo.Nombre.ToUpper().Trim();
                torneo.Deporte = torneo.Deporte.ToUpper().Trim();
                torneo.Modalidad = torneo.Modalidad.ToUpper().Trim();
                //torneo.Fixture.ForEach(f => _db.Entry(f).State = EntityState.Added);

                ValidadorTorneo validacion = new(_db);
                ValidationResult result = validacion.Validate(torneo);
                if (!result.IsValid)
                {
                    result.Errors.ForEach(f => mensajeError += f.ErrorMessage);
                    throw new Exception(mensajeError);
                }

                var nuevo = await _db.AddAsync(torneo);
            await _db.SaveChangesAsync();

            if (nuevo == null) throw new Exception("no se ha podido crear el torneo");

                return nuevo.Entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
