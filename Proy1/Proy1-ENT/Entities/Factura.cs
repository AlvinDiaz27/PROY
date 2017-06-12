using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class Factura : Comprobante
    {
        public int FacturaId { get; set; }
        public int Ruc { get; set; }
        public double Igv { get; set; }
        public double ImporteTotal { get; set; }
    }
}
