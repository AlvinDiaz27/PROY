using Proy1_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.EntityTypeConfigurations
{
    public class TarjetaConfiguration : EntityTypeConfiguration<Tarjeta>
    {
        public TarjetaConfiguration()
        {
            ToTable("Tarjeta");
            HasKey(b => b.TarjetaId);
        }
    }
}
