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
using Proy1_Per.Repository;

namespace Ventas.MVC.Controllers
{
    public class TipoController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;
        // GET: /Tipo/

        public TipoController()
        {

        }

        public TipoController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
         }


        public ActionResult Index()
        {
            //return View(db.Tipos.ToList());
            return View(_UnityOfWork.Tipos.GetAll());
        }

        // GET: /Tipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo tipo = _UnityOfWork.Tipos.Get(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        // GET: /Tipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TipoId,Nombre")] Tipo tipo)
        {
            if (ModelState.IsValid)
            {
               _UnityOfWork.Tipos.Add(tipo);
                // db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo);
        }

        // GET: /Tipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tipo tipo = db.Tipos.Find(id);
            Tipo tipo = _UnityOfWork.Tipos.Get(id);
            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        // POST: /Tipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TipoId,Nombre")] Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tipo);
                // db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo);
        }

        // GET: /Tipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tipo tipo = db.Tipos.Find(id);
            Tipo tipo = _UnityOfWork.Tipos.Get(id);

            if (tipo == null)
            {
                return HttpNotFound();
            }
            return View(tipo);
        }

        // POST: /Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Tipo tipo = db.Tipos.Find(id);
            //db.Tipos.Remove(tipo);
            //db.SaveChanges();
            Tipo tipo = _UnityOfWork.Tipos.Get(id);

            _UnityOfWork.Tipos.Delete(tipo);
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
