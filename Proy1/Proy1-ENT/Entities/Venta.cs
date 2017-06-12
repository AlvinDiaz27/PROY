using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class Venta
    {
        public int VentaId { get; set; }
        public double PrecioTotal { get; set; }
        public string Descripcion { get; set; }
        public List<DetalleVenta> DetalleVenta { get; set; }
        public Venta(List<DetalleVenta> detalleVenta)
        {
            DetalleVenta = detalleVenta;
        }
    }
}
