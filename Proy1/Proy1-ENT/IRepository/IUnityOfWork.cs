using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.IRepository
{
    public interface IUnityOfWork: IDisposable
    {
        IBoleta Boletas { get; }
        ICategoria Categorias { get; }
        ICliente Clientes { get; }
        IComprobante Comprobantes { get; }
        IDetalleVenta Detalles { get; }
        IEfectivo Efectivos { get; }
        IFactura Facturas { get; }
        IOrdenCompra Ordenes { get; }
        IPersona Personas { get; }
        IProducto Productos { get; }
        ITarjeta Tarjetas { get; }
        ITipo Tipos { get; }
        ITipo_Pago TipoPagos { get; }
        IVendedor Vendedores { get; }
        IVenta Ventas { get; }

        int SaveChanges();
        void StateModified(object entity);
    }
}
