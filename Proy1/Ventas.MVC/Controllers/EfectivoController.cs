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
    public class EfectivoController : Controller
    {
       // private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Efectivo/
         public EfectivoController()
        {

        }

        public EfectivoController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
            //return View(db.Tipos_Pagos.ToList());
            return View(_UnityOfWork.TipoPagos.GetAll());
        }

        // GET: /Efectivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Efectivo efectivo = db.Tipos_Pagos.Find(id);
            Efectivo efectivo =(Efectivo) _UnityOfWork.TipoPagos.Get(id);
            if (efectivo == null)
            {
                return HttpNotFound();
            }
            return View(efectivo);
        }

        // GET: /Efectivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Efectivo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Tipo_PagoId,Monto,EfectivoId")] Efectivo efectivo)
        {
            if (ModelState.IsValid)
            {
               // db.Tipos_Pagos.Add(efectivo);
                _UnityOfWork.TipoPagos.Add(efectivo);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(efectivo);
        }

        // GET: /Efectivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Efectivo efectivo = db.Tipos_Pagos.Find(id);
            Efectivo efectivo = (Efectivo)_UnityOfWork.TipoPagos.Get(id);
            if (efectivo == null)
            {
                return HttpNotFound();
            }
            return View(efectivo);
        }

        // POST: /Efectivo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Tipo_PagoId,Monto,EfectivoId")] Efectivo efectivo)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(efectivo).State = EntityState.Modified;
                _UnityOfWork.StateModified(efectivo);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(efectivo);
        }

        // GET: /Efectivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Efectivo efectivo = db.Tipos_Pagos.Find(id);
            Efectivo efectivo =(Efectivo) _UnityOfWork.TipoPagos.Get(id);
            if (efectivo == null)
            {
                return HttpNotFound();
            }
            return View(efectivo);
        }

        // POST: /Efectivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Efectivo efectivo = db.Tipos_Pagos.Find(id);
            Efectivo efectivo = (Efectivo)_UnityOfWork.TipoPagos.Get(id);
           // db.Tipos_Pagos.Remove(efectivo);
            _UnityOfWork.TipoPagos.Delete(efectivo);
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
