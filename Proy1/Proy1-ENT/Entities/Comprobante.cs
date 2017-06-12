using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class Comprobante
    {
        public int ComprobanteId { get; set; }
        public string Concepto { get; set; }
        public DateTime FechaEmision { get; set; }
        public List<Tipo_Pago> Tipo_Pago { get; set; }
        public Comprobante(List<Tipo_Pago> tipoPago)
        {
            Tipo_Pago = tipoPago;
        }
        public Comprobante()
        {

        }
    }
}
