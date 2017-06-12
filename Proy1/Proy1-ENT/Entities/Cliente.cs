using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
   public class Cliente : Persona
    {
        public int ClienteId { get; set; }
        public int Dni { get; set; }
        public List<Comprobante> Comprobante { get; set; }
        public Cliente(List<Comprobante> comprobante)
        {
            Comprobante = comprobante;
        }
    }
}
