using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class DetalleVenta
    {
        public int DetalleVentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Decripcion { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUni { get; set; }
        public List<Comprobante> Comprobante { get; set; }
        public DetalleVenta(List<Comprobante> comprabante)
        {
            Comprobante = comprabante;
        }
    }
}
