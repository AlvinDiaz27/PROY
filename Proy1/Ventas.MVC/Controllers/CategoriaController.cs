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
    public class CategoriaController : Controller
    {
        //private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Categoria/
        public CategoriaController()
        {

        }

        public CategoriaController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
           // return View(db.Categorias.ToList());
            return View(_UnityOfWork.Categorias.GetAll());
        }

        // GET: /Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Categoria categoria = db.Categorias.Find(id);
            Categoria categoria = _UnityOfWork.Categorias.Get(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: /Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Categoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CategoriaId,NombreCat")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Categorias.Add(categoria);
                //db.Categorias.Add(categoria);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: /Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Categoria categoria = db.Categorias.Find(id);
            Categoria categoria = _UnityOfWork.Categorias.Get(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: /Categoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CategoriaId,NombreCat")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(categoria);
                //db.Entry(categoria).State = EntityState.Modified;
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: /Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Categoria categoria = db.Categorias.Find(id);
            Categoria categoria = _UnityOfWork.Categorias.Get(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: /Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Categoria categoria = db.Categorias.Find(id);
            Categoria categoria = _UnityOfWork.Categorias.Get(id);
            //db.Categorias.Remove(categoria);
            _UnityOfWork.Categorias.Delete(categoria);
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
