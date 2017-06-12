using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class Boleta : Comprobante
    {
        public int BoletaId { get; set; }
        public double ImporteTotal { get; set; }
    }
}
