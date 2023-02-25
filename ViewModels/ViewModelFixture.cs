using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ViewModelFixture
    {
        public int TorneoId { get; set; }
        public List<PartidoVM> Fixture { get; set; }
    }
}
