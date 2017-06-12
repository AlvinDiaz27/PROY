using Proy1_ENT.Entities;
using Proy1_ENT.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.Repository
{
   public class EfectivoRepository:Repository <Efectivo>, IEfectivo
    {
       

       public EfectivoRepository(Proy1DbContext context):base(context)
       {
          
       }

      
    }
}
