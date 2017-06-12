using Proy1_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.EntityTypeConfigurations
{
    public class EfectivoConfiguration : EntityTypeConfiguration<Efectivo>
    {
        public EfectivoConfiguration()
        {
            ToTable("Efectivo");
            HasKey(b => b.EfectivoId);
        }
    }
}
