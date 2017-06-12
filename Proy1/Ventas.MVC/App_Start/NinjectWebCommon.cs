[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Ventas.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Ventas.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace Ventas.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    
    using Proy1_Per;
    using Proy1_ENT.IRepository;
    using Proy1_Per.Repository;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnityOfWork>().To<UnityOfWork>();
            kernel.Bind<Proy1DbContext>().To<Proy1DbContext>();

            kernel.Bind<ITipo>().To<TipoRepository>();
            kernel.Bind<IBoleta>().To<BoletaRepository>();
            kernel.Bind<ICategoria>().To<CategoriaRepository>();
            kernel.Bind<ICliente>().To<ClienteRepository>();
            kernel.Bind<IComprobante>().To<ComprobanteRepository>();
            kernel.Bind<IDetalleVenta>().To<DetalleVentaRepository>();
            kernel.Bind<IEfectivo>().To<EfectivoRepository>();
            kernel.Bind<IFactura>().To<FacturaRepository>();
            kernel.Bind<IOrdenCompra>().To<OrdenCompraRepository>();
            kernel.Bind<IPersona>().To<PersonaRepository>();
            kernel.Bind<IProducto>().To<ProductoRepository>();
            kernel.Bind<ITarjeta>().To<TarjetaRepository>();
            kernel.Bind<ITipo_Pago>().To<Tipo_PagoRepository>();
            kernel.Bind<IVendedor>().To<VendedorRepository>();
            kernel.Bind<IVenta>().To<VentaRepository>();
            

        }
        

    }
}
