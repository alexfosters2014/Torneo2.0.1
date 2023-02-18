using BaseDatosContext;
using Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Negocio.Validaciones
{
    public class ValidadorInscripcion : AbstractValidator<ViewModelInscripcion>
    {
        private readonly TorneoContext _db;
        public ValidadorInscripcion(TorneoContext _db)
        {
            this._db = _db;
            RuleFor(vw => vw.TorneoId).GreaterThan(0).WithMessage("Falta identificación de torneo");
            RuleFor(vw => vw.EquipoAInscribir).NotNull().NotEmpty().WithMessage("Debe seleccionar un equipo a inscribir")
                                              .Must(EquipoNoInscripto).WithMessage("El equipo ya fue inscripto");
        }

        private bool EquipoNoInscripto(ViewModelInscripcion vw, Equipo equipoAInscribir)
        {
            return !_db.Torneos.Any(to => to.Id == vw.TorneoId && to.Inscripciones.Any( eq => eq.Id == equipoAInscribir.Id));
        }
    }
}
