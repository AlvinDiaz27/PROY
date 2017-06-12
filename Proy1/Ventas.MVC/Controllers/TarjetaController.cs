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
    public class TarjetaController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Tarjeta/
         public TarjetaController()
        {

        }

        public TarjetaController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
           // return View(db.Tipos_Pagos.ToList());
            return View(_UnityOfWork.TipoPagos.GetAll());
        }

        // GET: /Tarjeta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tarjeta tarjeta = db.Tipos_Pagos.Find(id);
            Tarjeta tarjeta =(Tarjeta) _UnityOfWork.TipoPagos.Get(id);
            if (tarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tarjeta);
        }

        // GET: /Tarjeta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tarjeta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Tipo_PagoId,Monto,TarjetaId")] Tarjeta tarjeta)
        {
            if (ModelState.IsValid)
            {
                //db.Tipos_Pagos.Add(tarjeta);
                _UnityOfWork.TipoPagos.Add(tarjeta);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarjeta);
        }

        // GET: /Tarjeta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tarjeta tarjeta = db.Tipos_Pagos.Find(id);
            Tarjeta tarjeta = (Tarjeta)_UnityOfWork.TipoPagos.Get(id);
            if (tarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tarjeta);
        }

        // POST: /Tarjeta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Tipo_PagoId,Monto,TarjetaId")] Tarjeta tarjeta)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tarjeta).State = EntityState.Modified;
                _UnityOfWork.StateModified(tarjeta);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarjeta);
        }

        // GET: /Tarjeta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tarjeta tarjeta = db.Tipos_Pagos.Find(id);
            Tarjeta tarjeta = (Tarjeta)_UnityOfWork.TipoPagos.Get(id);
            if (tarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tarjeta);
        }

        // POST: /Tarjeta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Tarjeta tarjeta = db.Tipos_Pagos.Find(id);
            Tarjeta tarjeta = (Tarjeta)_UnityOfWork.TipoPagos.Get(id);
           // db.Tipos_Pagos.Remove(tarjeta);
            _UnityOfWork.TipoPagos.Delete(tarjeta);
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
