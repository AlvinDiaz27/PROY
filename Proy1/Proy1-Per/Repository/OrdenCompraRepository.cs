using Proy1_ENT.Entities;
using Proy1_ENT.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.Repository
{
    public class OrdenCompraRepository:Repository <OrdenCompra>, IOrdenCompra
    {
       

        public OrdenCompraRepository(Proy1DbContext context):base(context)
        {
            
        }

        
    }
}
