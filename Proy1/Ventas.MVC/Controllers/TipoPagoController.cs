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
    public class TipoPagoController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        
         public TipoPagoController()
        {

        }

        public TipoPagoController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: /TipoPago/
        public ActionResult Index()
        {
            //return View(db.Tipos_Pagos.ToList());
            return View(_UnityOfWork.TipoPagos.GetAll());

        }

        // GET: /TipoPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tipo_Pago tipo_pago = db.Tipos_Pagos.Find(id);
            Tipo_Pago tipo_pago = _UnityOfWork.TipoPagos.Get(id);
            if (tipo_pago == null)
            {
                return HttpNotFound();
            }
            return View(tipo_pago);
        }

        // GET: /TipoPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Tipo_PagoId,Monto")] Tipo_Pago tipo_pago)
        {
            if (ModelState.IsValid)
            {
                //db.Tipos_Pagos.Add(tipo_pago);
                _UnityOfWork.TipoPagos.Add(tipo_pago);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_pago);
        }

        // GET: /TipoPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tipo_Pago tipo_pago = db.Tipos_Pagos.Find(id);
            Tipo_Pago tipo_pago = _UnityOfWork.TipoPagos.Get(id);
            if (tipo_pago == null)
            {
                return HttpNotFound();
            }
            return View(tipo_pago);
        }

        // POST: /TipoPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Tipo_PagoId,Monto")] Tipo_Pago tipo_pago)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tipo_pago).State = EntityState.Modified;
                _UnityOfWork.StateModified(tipo_pago);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_pago);
        }

        // GET: /TipoPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tipo_Pago tipo_pago = db.Tipos_Pagos.Find(id);
            Tipo_Pago tipo_pago = _UnityOfWork.TipoPagos.Get(id);
            if (tipo_pago == null)
            {
                return HttpNotFound();
            }
            return View(tipo_pago);
        }

        // POST: /TipoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Tipo_Pago tipo_pago = db.Tipos_Pagos.Find(id);
            Tipo_Pago tipo_pago = _UnityOfWork.TipoPagos.Get(id);
            //db.Tipos_Pagos.Remove(tipo_pago);
            _UnityOfWork.TipoPagos.Delete(tipo_pago);
            //db.SaveChanges();
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose(); 
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
