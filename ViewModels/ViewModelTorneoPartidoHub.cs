using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ViewModelTorneoPartidoHub
    {
        public int TorneoId { get; set; }
        public PartidoVM PartidoVM { get; set; } = new();
        public bool PartidaEnProceso { get; set; } = false;
    }
}
