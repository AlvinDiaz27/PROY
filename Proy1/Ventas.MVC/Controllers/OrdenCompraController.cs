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
    public class OrdenCompraController : Controller
    {
       // private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;


        // GET: /OrdenCompra/
         public OrdenCompraController()
        {

        }

        public OrdenCompraController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
            //return View(db.OrdenCompras.ToList());
            return View(_UnityOfWork.Ordenes.GetAll());
        }

        // GET: /OrdenCompra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrdenCompra ordencompra = db.OrdenCompras.Find(id);
            OrdenCompra ordencompra = _UnityOfWork.Ordenes.Get(id);
            if (ordencompra == null)
            {
                return HttpNotFound();
            }
            return View(ordencompra);
        }

        // GET: /OrdenCompra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /OrdenCompra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OrdenCompraId,Decripcion,Cantidad,PrecioUni,Total")] OrdenCompra ordencompra)
        {
            if (ModelState.IsValid)
            {
                //db.OrdenCompras.Add(ordencompra);
                _UnityOfWork.Ordenes.Add(ordencompra);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ordencompra);
        }

        // GET: /OrdenCompra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrdenCompra ordencompra = db.OrdenCompras.Find(id);
            OrdenCompra ordencompra = _UnityOfWork.Ordenes.Get(id);
            if (ordencompra == null)
            {
                return HttpNotFound();
            }
            return View(ordencompra);
        }

        // POST: /OrdenCompra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrdenCompraId,Decripcion,Cantidad,PrecioUni,Total")] OrdenCompra ordencompra)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(ordencompra).State = EntityState.Modified;
                _UnityOfWork.StateModified(ordencompra);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ordencompra);
        }

        // GET: /OrdenCompra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrdenCompra ordencompra = db.OrdenCompras.Find(id);
            OrdenCompra ordencompra = _UnityOfWork.Ordenes.Get(id);
            if (ordencompra == null)
            {
                return HttpNotFound();
            }
            return View(ordencompra);
        }

        // POST: /OrdenCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //OrdenCompra ordencompra = db.OrdenCompras.Find(id);
            OrdenCompra ordencompra = _UnityOfWork.Ordenes.Get(id);
            //db.OrdenCompras.Remove(ordencompra);
            _UnityOfWork.Ordenes.Delete(ordencompra);
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
