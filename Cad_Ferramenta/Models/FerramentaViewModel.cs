using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cad_Ferramenta.Models
{
    public class FerramentaViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
        public string FabricanteDescricao { get; set; }
    }
}
