using Proy1_ENT.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_Per.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly Proy1DbContext _Context;

        public UnityOfWork(Proy1DbContext context)
        {
            _Context = context;

            Boletas = new BoletaRepository(_Context);
            Categorias = new CategoriaRepository(_Context);
            Clientes = new ClienteRepository(_Context);
            Comprobantes = new ComprobanteRepository(_Context);
            Detalles = new DetalleVentaRepository(_Context);
            Efectivos = new EfectivoRepository(_Context);
            Facturas = new FacturaRepository(_Context);
            Ordenes = new OrdenCompraRepository(_Context);
            Personas = new PersonaRepository(_Context);
            Productos = new ProductoRepository(_Context);
            Tarjetas = new TarjetaRepository(_Context);
            Tipos = new TipoRepository(_Context);
            TipoPagos = new Tipo_PagoRepository(_Context);
            Vendedores = new VendedorRepository(_Context);
            Ventas = new VentaRepository(_Context);
        }

        public IBoleta Boletas { get ; private set; }
        public ICategoria Categorias{ get ; private set; }
        public ICliente Clientes{ get ; private set; }
       public IComprobante Comprobantes { get ; private set; }
        public IDetalleVenta Detalles { get ; private set; }
        public IEfectivo Efectivos { get; private set; }
        public IFactura Facturas { get; private set; }
       public IOrdenCompra Ordenes { get ; private set; }
        public IPersona Personas { get ; private set; }
         public IProducto Productos { get ; private set; }
         public ITarjeta Tarjetas { get ; private set; }
        public ITipo Tipos { get ; private set; }
        public ITipo_Pago TipoPagos { get; private set; }
        public IVendedor Vendedores { get ; private set; }
         public IVenta Ventas { get; private set; }
         
        

       

         public int SaveChanges()
        {
            return _Context.SaveChanges();
        }

         public void Dispose()
        {
            _Context.Dispose();

        }

         public void StateModified(object Entity)
         {
             _Context.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
         }
    }
}
