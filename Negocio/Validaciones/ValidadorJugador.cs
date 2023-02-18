using Azure.Storage.Blobs.Models;
using BaseDatosContext;
using Entidades;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Negocio.Validaciones
{
    public class ValidadorJugador : AbstractValidator<Jugador>
    {
        private readonly TorneoContext _db;
        public ValidadorJugador(TorneoContext _db)
        {
            this._db = _db;

            RuleFor(jug => jug.Cedula).NotEmpty().Must(ValidarCedula).WithMessage("La cédula es incorrecta")
                                                 .Must(JugadorNoExiste).WithMessage("El jugador ya existe en el sistema");
            RuleFor(jug => jug.Nombres).NotEmpty().WithMessage("Debe ingresar un nombre");
            RuleFor(jug => jug.Apellidos).NotEmpty().WithMessage("Debe ingresar un apellido");
            RuleFor(jug => jug.FechaNacimiento).NotNull().NotEmpty().Must(ValidarEdad).WithMessage("La edad está fuera de rango");
        }

        private bool ValidarCedula(string cedula)
        {
            return ValidadorCedula.Validar(cedula.Trim());
        }

        private bool ValidarEdad(DateTime fechaNacimiento)
        {
            int cantidadAnios = DateTime.Today.Year - fechaNacimiento.Year;
            return cantidadAnios > 0 && cantidadAnios < 90;
        }

        private bool JugadorNoExiste(string cedula)
        {
            return !_db.Jugadores.Any(c => c.Cedula == cedula.Trim());
        }


    }
}
