using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1_ENT.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public double PrecioVenta { get; set; }
        public DetalleVenta DetalleVenta { get; set; }
        public List<OrdenCompra> OrdenCompra { get; set; }
        public Categoria Categoria { get; set; }
        public Tipo Tipo { get; set; }
        public Producto(DetalleVenta detalleVenta,Categoria categoria, Tipo tipo)
        {
            Tipo = tipo;
            Categoria = Categoria;
            DetalleVenta= detalleVenta;
            OrdenCompra = new List<OrdenCompra>();

        }
    }
}
