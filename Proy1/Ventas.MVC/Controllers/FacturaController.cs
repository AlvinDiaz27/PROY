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
    public class FacturaController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Factura/

         public FacturaController()
        {

        }

        public FacturaController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
            //return View(db.Comprobantes.ToList());
            return View(_UnityOfWork.Comprobantes.GetAll());
        }

        // GET: /Factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Factura factura = db.Comprobantes.Find(id);
            Factura factura = (Factura)_UnityOfWork.Comprobantes.Get(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: /Factura/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Factura/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ComprobanteId,Concepto,FechaEmision,FacturaId,Ruc,Igv,ImporteTotal")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                //db.Comprobantes.Add(factura);
                _UnityOfWork.Comprobantes.Add(factura);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(factura);
        }

        // GET: /Factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Factura factura = db.Comprobantes.Find(id);
            Factura factura = (Factura)_UnityOfWork.Comprobantes.Get(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: /Factura/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ComprobanteId,Concepto,FechaEmision,FacturaId,Ruc,Igv,ImporteTotal")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(factura).State = EntityState.Modified;
                _UnityOfWork.StateModified(factura);
               // db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(factura);
        }

        // GET: /Factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Factura factura = db.Comprobantes.Find(id);
            Factura factura = (Factura)_UnityOfWork.Comprobantes.Get(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: /Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Factura factura = db.Comprobantes.Find(id);
            Factura factura = (Factura)_UnityOfWork.Comprobantes.Get(id);
            //db.Comprobantes.Remove(factura);
            _UnityOfWork.Comprobantes.Delete(factura);
            //db.SaveChanges();
            _UnityOfWork.SaveChanges();
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
