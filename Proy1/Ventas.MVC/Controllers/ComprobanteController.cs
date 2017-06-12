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
    public class ComprobanteController : Controller
    {
       // private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Comprobante/

        public ComprobanteController()
        {

        }

        public ComprobanteController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        
        public ActionResult Index()
        {
           // return View(db.Comprobantes.ToList());
            return View(_UnityOfWork.Comprobantes.GetAll());
        }

        // GET: /Comprobante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comprobante comprobante = db.Comprobantes.Find(id);
            Comprobante comprobante = _UnityOfWork.Comprobantes.Get(id);

            if (comprobante == null)
            {
                return HttpNotFound();
            }
            return View(comprobante);
        }

        // GET: /Comprobante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Comprobante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ComprobanteId,Concepto,FechaEmision")] Comprobante comprobante)
        {
            if (ModelState.IsValid)
            {
                //db.Comprobantes.Add(comprobante);
                _UnityOfWork.Comprobantes.Add(comprobante);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comprobante);
        }

        // GET: /Comprobante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comprobante comprobante = db.Comprobantes.Find(id);
            Comprobante comprobante = _UnityOfWork.Comprobantes.Get(id);
            if (comprobante == null)
            {
                return HttpNotFound();
            }
            return View(comprobante);
        }

        // POST: /Comprobante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ComprobanteId,Concepto,FechaEmision")] Comprobante comprobante)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(comprobante).State = EntityState.Modified;
                _UnityOfWork.StateModified(comprobante);
               // db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comprobante);
        }

        // GET: /Comprobante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comprobante comprobante = db.Comprobantes.Find(id);
            Comprobante comprobante = _UnityOfWork.Comprobantes.Get(id);
            if (comprobante == null)
            {
                return HttpNotFound();
            }
            return View(comprobante);
        }

        // POST: /Comprobante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Comprobante comprobante = db.Comprobantes.Find(id);
            Comprobante comprobante = _UnityOfWork.Comprobantes.Get(id);
            //db.Comprobantes.Remove(comprobante);
            _UnityOfWork.Comprobantes.Delete(comprobante);
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
