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
    public class ValidadorTorneo : AbstractValidator<Torneo>
    {
        private readonly TorneoContext _db;
        public ValidadorTorneo(TorneoContext _db)
        {
            this._db = _db;

            RuleFor(t => t.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio").Must(TorneoNoExiste).WithMessage("Ya existe el torneo");
            RuleFor(t => t.Desde).NotNull().Must(ValidacionFecha).WithMessage("Los rangos de fechas son incorrectos");
            RuleFor(t => t.Deporte).NotEmpty().WithMessage("El campo deporte no puede estar vacio");
            RuleFor(t => t.SetsMax).GreaterThan(0).WithMessage("El set maáximo debe ser mayor a cero");
            RuleFor(t => t.SetsMax).GreaterThan(0).WithMessage("el puntaje máximo debe ser mayor a cero");
        }

        private bool TorneoNoExiste(string nombre)
        {
            return !_db.Torneos.Any(c => c.Nombre == nombre.Trim());
        }

        private bool ValidacionFecha(Torneo torneo, DateTime desde)
        {
            return (desde.CompareTo(torneo.Hasta) <= 0 && desde.CompareTo(DateTime.Today) >= 0);
        }
    }
}
