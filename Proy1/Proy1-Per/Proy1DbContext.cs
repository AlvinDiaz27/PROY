using Proy1_ENT.Entities;
using Proy1_Per.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per
{
    public class Proy1DbContext : DbContext
    {
        public DbSet<Boleta> Boletas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Efectivo> Efectivos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<OrdenCompra> OrdenCompras { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Tipo_Pago> Tipos_Pagos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            
            
            
            modelBuilder.Configurations.Add(new BoletaConfiguration());
            modelBuilder.Configurations.Add(new CategoriaConfiguration());
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ComprobanteConfiguration());
            modelBuilder.Configurations.Add(new DetalleVentaConfiguration());
            modelBuilder.Configurations.Add(new EfectivoConfiguration());
            modelBuilder.Configurations.Add(new FacturaConfiguration());
            modelBuilder.Configurations.Add(new OrdenCompraConfiguration());
            modelBuilder.Configurations.Add(new PersonaConfiguration());
            modelBuilder.Configurations.Add(new ProductoConfiguration());
            modelBuilder.Configurations.Add(new TarjetaConfiguration());
            modelBuilder.Configurations.Add(new TipoConfiguration());
            modelBuilder.Configurations.Add(new VendedorConfiguration());
            modelBuilder.Configurations.Add(new VentaConfiguration());
            modelBuilder.Configurations.Add(new Tipo_PagoConfiguration());

            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
