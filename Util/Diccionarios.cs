using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Diccionarios
    {
        public static Dictionary<string, int> TipoTorneo = new()
        {
            {"VOLEY",6},
            {"FUTBOL",11}
        };

        public static readonly string LOCAL = "LOCAL";
        public static readonly string VISITANTE = "VISITANTE";

    }
}
