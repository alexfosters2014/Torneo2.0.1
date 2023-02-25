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

        public async Task<List<ViewModelTorneo>> GetTorneosDeporteInscripcion(string deporte)
        {
            return await _db.Torneos.Where(t => t.Deporte == deporte)
                              .AsNoTracking()
                              .Select(s => new ViewModelTorneo()
                              {
                                    Id = s.Id,
                                    Nombre= s.Nombre,
                              })
                              .ToListAsync();


        }

        public async Task<List<ViewModelTorneo>> GetTorneosVigentes()
        {
            var torneos = await _db.Torneos.Include(i => i.Fixture)
                                     .AsNoTracking()
                                     .Where(w => w.Desde >= DateTime.Today)
                                     .Select(s => new ViewModelTorneo()
                                     {
                                         Id = s.Id,
                                         Nombre = s.Nombre,
                                         ImagenRef = s.ImagenRef,
                                         Modalidad = s.Modalidad,
                                         SetsMax = s.SetsMax,
                                         PuntajeMax = s.PuntajeMax,
                                         PuntajeMaxDefinitorio = s.PuntajeMaxDefinitorio,
                                         Deporte = s.Deporte,
                                         Desde = s.Desde,
                                         Hasta = s.Hasta,
                                         Fixture = s.Fixture.Select(s => new PartidoVM()
                                         {
                                              Id= s.Id,
                                              Fecha=s.Fecha,
                                              Guid= s.Guid,
                                              HistorialPartido= s.HistorialPartido,
                                              Local = s.Local == null ? null : new EquipoVM()
                                              {
                                                Id = s.Local.Id,
                                                Caratula = s.Local.Caratula,
                                                Deporte = s.Local.Deporte,
                                                NombreEquipo = s.Local.NombreEquipo
                                              },
                                              Visitante = s.Visitante == null ? null : new EquipoVM()
                                              {
                                                  Id = s.Visitante.Id,
                                                  Caratula = s.Visitante.Caratula,
                                                  Deporte = s.Visitante.Deporte,
                                                  NombreEquipo = s.Visitante.NombreEquipo
                                              },
                                               VisitanteId = s.VisitanteId,
                                               LocalId = s.LocalId,
                                               Lugar= s.Lugar,
                                               MarcadorLocal= s.MarcadorLocal,
                                               MarcadorVisitante= s.MarcadorVisitante,
                                               NombreCancha= s.NombreCancha,
                                               Orden = s.Orden,
                                               PartidoSiguienteGuid= s.PartidoSiguienteGuid,
                                               Posición = s.Posición,
                                               PuntajeLocal= s.PuntajeLocal,
                                               PuntajeVisitante= s.PuntajeVisitante,
                                               Ronda = s.Ronda,
                                               RondaDescanso= s.RondaDescanso,
                                               SetActual = s.SetActual,
                                               SetsGanadosLocal= s.SetsGanadosLocal,
                                               SetsGanadosVisitante = s.SetsGanadosVisitante
                                         }).ToList(),
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
                              .AsNoTracking()
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

        public async Task<bool> GuardarFixtureCompleto(int torneoId,List<PartidoVM> partidosVM)
        {
            try
            {
                var torneoDB = await _db.Torneos.FindAsync(torneoId);
                if (torneoDB == null) throw new Exception("No se encuentra el torneo seleccionado");

                torneoDB.Fixture = partidosVM.Select(s => new Partido()
                {
                    Id = s.Id,
                    Fecha = s.Fecha,
                    Guid = s.Guid,
                    HistorialPartido = s.HistorialPartido,
                    Local = s.Local == null ? null : new Equipo()
                    {
                        Id = s.Local.Id,
                        Caratula = s.Local.Caratula,
                        Deporte = s.Local.Deporte,
                        NombreEquipo = s.Local.NombreEquipo
                    },
                    Visitante = s.Visitante == null ? null : new Equipo()
                    {
                        Id = s.Visitante.Id,
                        Caratula = s.Visitante.Caratula,
                        Deporte = s.Visitante.Deporte,
                        NombreEquipo = s.Visitante.NombreEquipo
                    },
                    Lugar = s.Lugar,
                    MarcadorLocal = s.MarcadorLocal,
                    MarcadorVisitante = s.MarcadorVisitante,
                    NombreCancha = s.NombreCancha,
                    Orden = s.Orden,
                    PartidoSiguienteGuid = s.PartidoSiguienteGuid,
                    Posición = s.Posición,
                    PuntajeLocal = s.PuntajeLocal,
                    PuntajeVisitante = s.PuntajeVisitante,
                    Ronda = s.Ronda,
                    RondaDescanso = s.RondaDescanso,
                    SetActual = s.SetActual,
                    SetsGanadosLocal = s.SetsGanadosLocal,
                    SetsGanadosVisitante = s.SetsGanadosVisitante
                }).ToList();

                foreach (var partido in torneoDB.Fixture)
                {
                    if (partido.Local != null)
                    {
                    _db.Entry(partido.Local).State = EntityState.Unchanged;

                    }

                    if (partido.Visitante != null)
                    {
                        _db.Entry(partido.Visitante).State = EntityState.Unchanged;
                    }
                    _db.Entry(partido).State = EntityState.Added;
                }

                _db.Update(torneoDB);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarPartido(PartidoVM partidoVM)
        {
            try
            {
                var partidoDB = await _db.Partidos.FindAsync(partidoVM.Id);
                if (partidoDB == null) throw new Exception("No se encuentra el partido seleccionado");

               
                    partidoDB.Id = partidoVM.Id;
                    partidoDB.Fecha = partidoVM.Fecha;
                    partidoDB.Guid = partidoVM.Guid;
                    partidoDB.HistorialPartido = partidoVM.HistorialPartido;
                    partidoDB.Local = partidoVM.Local == null ? null : new Equipo()
                    {
                        Id = partidoVM.Local.Id,
                        Caratula = partidoVM.Local.Caratula,
                        Deporte = partidoVM.Local.Deporte,
                        NombreEquipo = partidoVM.Local.NombreEquipo
                    };
                     partidoDB.Visitante = partidoVM.Visitante == null ? null : new Equipo()
                    {
                        Id = partidoVM.Visitante.Id,
                        Caratula = partidoVM.Visitante.Caratula,
                        Deporte = partidoVM.Visitante.Deporte,
                        NombreEquipo = partidoVM.Visitante.NombreEquipo
                    };
                    partidoDB.Lugar = partidoVM.Lugar;
                    partidoDB.MarcadorLocal = partidoVM.MarcadorLocal;
                    partidoDB.MarcadorVisitante = partidoVM.MarcadorVisitante;
                    partidoDB.NombreCancha = partidoVM.NombreCancha;
                    partidoDB.Orden = partidoVM.Orden;
                    partidoDB.PartidoSiguienteGuid = partidoVM.PartidoSiguienteGuid;
                    partidoDB.Posición = partidoVM.Posición;
                    partidoDB.PuntajeLocal = partidoVM.PuntajeLocal;
                    partidoDB.PuntajeVisitante = partidoVM.PuntajeVisitante;
                    partidoDB.Ronda = partidoVM.Ronda;
                    partidoDB.RondaDescanso = partidoVM.RondaDescanso;
                    partidoDB.SetActual = partidoVM.SetActual;
                    partidoDB.SetsGanadosLocal = partidoVM.SetsGanadosLocal;
                    partidoDB.SetsGanadosVisitante = partidoVM.SetsGanadosVisitante;

                    if (partidoDB.Local != null)
                    {
                        _db.Entry(partidoDB.Local).State = EntityState.Unchanged;
                    }

                    if (partidoDB.Visitante != null)
                    {
                        _db.Entry(partidoDB.Visitante).State = EntityState.Unchanged;
                    }

                _db.Partidos.Update(partidoDB);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
