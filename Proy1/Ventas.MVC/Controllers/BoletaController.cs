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
    public class BoletaController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Boleta/

        public BoletaController()
        {

        }

        public BoletaController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        public ActionResult Index()
        {
            //return View(db.Comprobantes.ToList());
            return View(_UnityOfWork.Comprobantes.GetAll());
        }

        // GET: /Boleta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Boleta boleta = db.Comprobantes.Find(id);
            Boleta boleta = (Boleta)_UnityOfWork.Comprobantes.Get(id);
            if (boleta == null)
            {
                return HttpNotFound();
            }
            return View(boleta);
        }

        // GET: /Boleta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Boleta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ComprobanteId,Concepto,FechaEmision,BoletaId,ImporteTotal")] Boleta boleta)
        {
            if (ModelState.IsValid)
            {

                _UnityOfWork.Comprobantes.Add(boleta);
                // db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");

                //db.Comprobantes.Add(boleta);
                //db.SaveChanges();
                
            }

            return View(boleta);
        }

        // GET: /Boleta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Boleta boleta = db.Comprobantes.Find(id);
            Boleta boleta = (Boleta)_UnityOfWork.Comprobantes.Get(id);
            if (boleta == null)
            {
                return HttpNotFound();
            }
            return View(boleta);
        }

        // POST: /Boleta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ComprobanteId,Concepto,FechaEmision,BoletaId,ImporteTotal")] Boleta boleta)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(boleta);
                // db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
                
                
                
               
            }
            return View(boleta);
        }

        // GET: /Boleta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Boleta boleta = db.Comprobantes.Find(id);
            Boleta boleta=(Boleta)_UnityOfWork.Comprobantes.Get(id);

            if (boleta == null)
            {
                return HttpNotFound();
            }
            return View(boleta);
        }

        // POST: /Boleta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            //Boleta boleta = db.Comprobantes.Find(id);
            Boleta boleta = (Boleta)_UnityOfWork.Comprobantes.Get(id);

            //db.Comprobantes.Remove(boleta);
            _UnityOfWork.Boletas.Delete(boleta);

            //db.SaveChanges();
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
