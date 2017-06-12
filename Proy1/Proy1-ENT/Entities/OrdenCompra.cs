using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class OrdenCompra
    {
        public int OrdenCompraId { get; set; }
        public string Decripcion { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUni { get; set; }
        public double Total { get; set; }
        
    }
}
