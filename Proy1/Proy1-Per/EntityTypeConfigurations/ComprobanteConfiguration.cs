using Proy1_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.EntityTypeConfigurations
{
    public class ComprobanteConfiguration : EntityTypeConfiguration<Comprobante>
    {
        public ComprobanteConfiguration()
        {
            ToTable("Comprobante");
            HasKey(b => b.ComprobanteId);
        }
    }
}
