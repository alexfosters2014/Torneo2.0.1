using BaseDatosContext;
using Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Negocio.Validaciones
{
    public class ValidadorEquipo : AbstractValidator<Equipo>
    {
        private readonly TorneoContext _db;
        public ValidadorEquipo(TorneoContext _db)
        {
            this._db = _db;

            RuleFor(eq => eq.Caratula).NotNull().NotEmpty().WithMessage("El equipo debe tener un logo que los identifique");
            RuleFor(eq => eq.Deporte).NotEmpty();
            RuleFor(eq => eq.Deporte).NotEmpty().Must(CantidadJugadoresCorrecto).WithMessage("El equipo que quiere registrar está incorrecto. Puede que le falten jugadores o sobren");
            RuleFor(eq => eq.NombreEquipo).NotEmpty().MaximumLength(50)
                                           .Must(EquipoNoExiste).WithMessage("Ha excedido la cantidad máxima de 50 caracteres para el nombre dekl equipo");

        }
        private bool EquipoNoExiste(string nombreEquipo)
        {
            return !_db.Equipos.Any(c => c.NombreEquipo == nombreEquipo.ToUpper().Trim());
        }
        private bool CantidadJugadoresCorrecto(Equipo equipo, string deporte)
        {
            // return equipo.Jugadores.Count == Diccionarios.TipoTorneo[deporte];
            return true;
        }
    }
}
