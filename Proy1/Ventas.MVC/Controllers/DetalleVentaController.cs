using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proy1_ENT.Entities;
using Proy1_Per;
using Proy1_ENT.IRepository;

namespace Ventas.MVC.Controllers
{
    public class DetalleVentaController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /DetalleVenta/
        public DetalleVentaController()
        {

        }

        public DetalleVentaController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
            //return View(db.DetalleVentas.ToList());
            return View(_UnityOfWork.Detalles.GetAll());
        }

        // GET: /DetalleVenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DetalleVenta detalleventa = db.DetalleVentas.Find(id);
            DetalleVenta detalleventa = _UnityOfWork.Detalles.Get(id);

            if (detalleventa == null)
            {
                return HttpNotFound();
            }
            return View(detalleventa);
        }

        // GET: /DetalleVenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DetalleVenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DetalleVentaId,Fecha,Decripcion,Cantidad,PrecioUni")] DetalleVenta detalleventa)
        {
            if (ModelState.IsValid)
            {
                //db.DetalleVentas.Add(detalleventa);
                _UnityOfWork.Detalles.Add(detalleventa);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detalleventa);
        }

        // GET: /DetalleVenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DetalleVenta detalleventa = db.DetalleVentas.Find(id);
            DetalleVenta detalleventa = _UnityOfWork.Detalles.Get(id);
            if (detalleventa == null)
            {
                return HttpNotFound();
            }
            return View(detalleventa);
        }

        // POST: /DetalleVenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DetalleVentaId,Fecha,Decripcion,Cantidad,PrecioUni")] DetalleVenta detalleventa)
        {
            if (ModelState.IsValid)
            {
                
                //db.Entry(detalleventa).State = EntityState.Modified;
                _UnityOfWork.StateModified(detalleventa);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detalleventa);
        }

        // GET: /DetalleVenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DetalleVenta detalleventa = db.DetalleVentas.Find(id);
            DetalleVenta detalleventa = _UnityOfWork.Detalles.Get(id);

            if (detalleventa == null)
            {
                return HttpNotFound();
            }
            return View(detalleventa);
        }

        // POST: /DetalleVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            //DetalleVenta detalleventa = db.DetalleVentas.Find(id);
            DetalleVenta detalleventa = _UnityOfWork.Detalles.Get(id);
            //db.DetalleVentas.Remove(detalleventa);
            _UnityOfWork.Detalles.Delete(detalleventa);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                _UnityOfWork.Dispose();            
            }
            base.Dispose(disposing);
        }
    }
}
