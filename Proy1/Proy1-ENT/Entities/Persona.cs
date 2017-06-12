using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class Persona
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
        public string Correo { get; set; }
        public char Sexo { get; set; }

    }
}
