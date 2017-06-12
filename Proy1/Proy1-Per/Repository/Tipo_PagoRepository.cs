using Proy1_ENT.Entities;
using Proy1_ENT.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.Repository
{
    public class Tipo_PagoRepository:Repository <Tipo_Pago>, ITipo_Pago
    {
       

        public Tipo_PagoRepository(Proy1DbContext context):base(context)
        {
            
        }

       
    }
}
