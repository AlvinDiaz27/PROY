namespace Proy1_Per.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comprobante",
                c => new
                    {
                        ComprobanteId = c.Int(nullable: false, identity: true),
                        Concepto = c.String(),
                        FechaEmision = c.DateTime(nullable: false),
                        Cliente_PersonaId = c.Int(),
                        DetalleVenta_DetalleVentaId = c.Int(),
                    })
                .PrimaryKey(t => t.ComprobanteId)
                .ForeignKey("dbo.Cliente", t => t.Cliente_PersonaId)
                .ForeignKey("dbo.DetalleVenta", t => t.DetalleVenta_DetalleVentaId)
                .Index(t => t.Cliente_PersonaId)
                .Index(t => t.DetalleVenta_DetalleVentaId);
            
            CreateTable(
                "dbo.Tipo_Pago",
                c => new
                    {
                        Tipo_PagoId = c.Int(nullable: false, identity: true),
                        Monto = c.Double(nullable: false),
                        Comprobante_ComprobanteId = c.Int(),
                    })
                .PrimaryKey(t => t.Tipo_PagoId)
                .ForeignKey("dbo.Comprobante", t => t.Comprobante_ComprobanteId)
                .Index(t => t.Comprobante_ComprobanteId);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        NombreCat = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        PersonaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        ApePaterno = c.String(),
                        ApeMaterno = c.String(),
                        Direccion = c.String(),
                        Telefono = c.Long(nullable: false),
                        Correo = c.String(),
                    })
                .PrimaryKey(t => t.PersonaId);
            
            CreateTable(
                "dbo.DetalleVenta",
                c => new
                    {
                        DetalleVentaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Decripcion = c.String(),
                        Cantidad = c.Int(nullable: false),
                        PrecioUni = c.Double(nullable: false),
                        Venta_VentaId = c.Int(),
                    })
                .PrimaryKey(t => t.DetalleVentaId)
                .ForeignKey("dbo.Venta", t => t.Venta_VentaId)
                .Index(t => t.Venta_VentaId);
            
            CreateTable(
                "dbo.OrdenCompra",
                c => new
                    {
                        OrdenCompraId = c.Int(nullable: false, identity: true),
                        Decripcion = c.String(),
                        Cantidad = c.Int(nullable: false),
                        PrecioUni = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Producto_ProductoId = c.Int(),
                    })
                .PrimaryKey(t => t.OrdenCompraId)
                .ForeignKey("dbo.Producto", t => t.Producto_ProductoId)
                .Index(t => t.Producto_ProductoId);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        PrecioTotal = c.Double(nullable: false),
                        Descripcion = c.String(),
                        Vendedor_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Vendedor", t => t.Vendedor_PersonaId)
                .Index(t => t.Vendedor_PersonaId);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        PrecioVenta = c.Double(nullable: false),
                        Categoria_CategoriaId = c.Int(),
                        DetalleVenta_DetalleVentaId = c.Int(),
                        Tipo_TipoId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Categoria", t => t.Categoria_CategoriaId)
                .ForeignKey("dbo.DetalleVenta", t => t.DetalleVenta_DetalleVentaId)
                .ForeignKey("dbo.Tipo", t => t.Tipo_TipoId)
                .Index(t => t.Categoria_CategoriaId)
                .Index(t => t.DetalleVenta_DetalleVentaId)
                .Index(t => t.Tipo_TipoId);
            
            CreateTable(
                "dbo.Tipo",
                c => new
                    {
                        TipoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.TipoId);
            
            CreateTable(
                "dbo.Boleta",
                c => new
                    {
                        ComprobanteId = c.Int(nullable: false),
                        BoletaId = c.Int(nullable: false),
                        ImporteTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ComprobanteId)
                .ForeignKey("dbo.Comprobante", t => t.ComprobanteId)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        PersonaId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        Dni = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonaId)
                .ForeignKey("dbo.Persona", t => t.PersonaId)
                .Index(t => t.PersonaId);
            
            CreateTable(
                "dbo.Efectivo",
                c => new
                    {
                        Tipo_PagoId = c.Int(nullable: false),
                        EfectivoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Tipo_PagoId)
                .ForeignKey("dbo.Tipo_Pago", t => t.Tipo_PagoId)
                .Index(t => t.Tipo_PagoId);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        ComprobanteId = c.Int(nullable: false),
                        FacturaId = c.Int(nullable: false),
                        Ruc = c.Int(nullable: false),
                        Igv = c.Double(nullable: false),
                        ImporteTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ComprobanteId)
                .ForeignKey("dbo.Comprobante", t => t.ComprobanteId)
                .Index(t => t.ComprobanteId);
            
            CreateTable(
                "dbo.Tarjeta",
                c => new
                    {
                        Tipo_PagoId = c.Int(nullable: false),
                        TarjetaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Tipo_PagoId)
                .ForeignKey("dbo.Tipo_Pago", t => t.Tipo_PagoId)
                .Index(t => t.Tipo_PagoId);
            
            CreateTable(
                "dbo.Vendedor",
                c => new
                    {
                        PersonaId = c.Int(nullable: false),
                        VendedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonaId)
                .ForeignKey("dbo.Persona", t => t.PersonaId)
                .Index(t => t.PersonaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendedor", "PersonaId", "dbo.Persona");
            DropForeignKey("dbo.Tarjeta", "Tipo_PagoId", "dbo.Tipo_Pago");
            DropForeignKey("dbo.Factura", "ComprobanteId", "dbo.Comprobante");
            DropForeignKey("dbo.Efectivo", "Tipo_PagoId", "dbo.Tipo_Pago");
            DropForeignKey("dbo.Cliente", "PersonaId", "dbo.Persona");
            DropForeignKey("dbo.Boleta", "ComprobanteId", "dbo.Comprobante");
            DropForeignKey("dbo.Producto", "Tipo_TipoId", "dbo.Tipo");
            DropForeignKey("dbo.OrdenCompra", "Producto_ProductoId", "dbo.Producto");
            DropForeignKey("dbo.Producto", "DetalleVenta_DetalleVentaId", "dbo.DetalleVenta");
            DropForeignKey("dbo.Producto", "Categoria_CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Venta", "Vendedor_PersonaId", "dbo.Vendedor");
            DropForeignKey("dbo.DetalleVenta", "Venta_VentaId", "dbo.Venta");
            DropForeignKey("dbo.Comprobante", "DetalleVenta_DetalleVentaId", "dbo.DetalleVenta");
            DropForeignKey("dbo.Comprobante", "Cliente_PersonaId", "dbo.Cliente");
            DropForeignKey("dbo.Tipo_Pago", "Comprobante_ComprobanteId", "dbo.Comprobante");
            DropIndex("dbo.Vendedor", new[] { "PersonaId" });
            DropIndex("dbo.Tarjeta", new[] { "Tipo_PagoId" });
            DropIndex("dbo.Factura", new[] { "ComprobanteId" });
            DropIndex("dbo.Efectivo", new[] { "Tipo_PagoId" });
            DropIndex("dbo.Cliente", new[] { "PersonaId" });
            DropIndex("dbo.Boleta", new[] { "ComprobanteId" });
            DropIndex("dbo.Producto", new[] { "Tipo_TipoId" });
            DropIndex("dbo.Producto", new[] { "DetalleVenta_DetalleVentaId" });
            DropIndex("dbo.Producto", new[] { "Categoria_CategoriaId" });
            DropIndex("dbo.Venta", new[] { "Vendedor_PersonaId" });
            DropIndex("dbo.OrdenCompra", new[] { "Producto_ProductoId" });
            DropIndex("dbo.DetalleVenta", new[] { "Venta_VentaId" });
            DropIndex("dbo.Tipo_Pago", new[] { "Comprobante_ComprobanteId" });
            DropIndex("dbo.Comprobante", new[] { "DetalleVenta_DetalleVentaId" });
            DropIndex("dbo.Comprobante", new[] { "Cliente_PersonaId" });
            DropTable("dbo.Vendedor");
            DropTable("dbo.Tarjeta");
            DropTable("dbo.Factura");
            DropTable("dbo.Efectivo");
            DropTable("dbo.Cliente");
            DropTable("dbo.Boleta");
            DropTable("dbo.Tipo");
            DropTable("dbo.Producto");
            DropTable("dbo.Venta");
            DropTable("dbo.OrdenCompra");
            DropTable("dbo.DetalleVenta");
            DropTable("dbo.Persona");
            DropTable("dbo.Categoria");
            DropTable("dbo.Tipo_Pago");
            DropTable("dbo.Comprobante");
        }
    }
}
