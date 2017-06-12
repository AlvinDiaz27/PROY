using Proy1_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.EntityTypeConfigurations
{
    public class FacturaConfiguration : EntityTypeConfiguration<Factura>
    {
        public FacturaConfiguration()
        {
            ToTable("Factura");
            HasKey(b => b.FacturaId);
        }
    }
}
