using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
   public class Vendedor : Persona
    {
        public int VendedorId { get; set; }
        public List<Venta> Venta { get; set; }
        public Vendedor(List<Venta> venta)
        {
            Venta = venta;
        }
    }
}
