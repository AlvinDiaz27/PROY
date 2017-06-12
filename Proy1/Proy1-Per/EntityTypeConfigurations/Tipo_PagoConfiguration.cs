using Proy1_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.EntityTypeConfigurations
{
    public class Tipo_PagoConfiguration : EntityTypeConfiguration<Tipo_Pago>
    {
        public Tipo_PagoConfiguration()
        {
            ToTable("Tipo_Pago");
            HasKey(b => b.Tipo_PagoId);
        }
    }
}
