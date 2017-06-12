using Proy1_ENT.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.EntityTypeConfigurations
{
    public class VendedorConfiguration : EntityTypeConfiguration<Vendedor>
    {
        public VendedorConfiguration()
        {
            ToTable("Vendedor");
            HasKey(b => b.VendedorId);
        }
    }
}
