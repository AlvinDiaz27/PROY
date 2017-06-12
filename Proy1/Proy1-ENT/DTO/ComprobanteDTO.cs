using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.DTO
{
    public class FacturaDTO
    {
        public int ComprobanteId { get; set; }
        public string Concepto { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
