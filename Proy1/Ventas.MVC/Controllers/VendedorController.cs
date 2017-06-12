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
    public class VendedorController : Controller
    {
       // private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public VendedorController()
        {

        }

        public VendedorController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }


        // GET: /Vendedor/
        public ActionResult Index()
        {
            //return View(db.Personas.ToList());
            return View(_UnityOfWork.Personas.GetAll());
        }

        // GET: /Vendedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vendedor vendedor = db.Personas.Find(id);
            Vendedor vendedor= (Vendedor)_UnityOfWork.Personas.Get(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        // GET: /Vendedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Vendedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PersonaId,Nombre,ApePaterno,ApeMaterno,Direccion,Telefono,Correo,VendedorId")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
               // db.Personas.Add(vendedor);
                _UnityOfWork.Personas.Add(vendedor);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendedor);
        }

        // GET: /Vendedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vendedor vendedor = db.Personas.Find(id);
            Vendedor vendedor = (Vendedor)_UnityOfWork.Personas.Get(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        // POST: /Vendedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PersonaId,Nombre,ApePaterno,ApeMaterno,Direccion,Telefono,Correo,VendedorId")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(vendedor).State = EntityState.Modified;
                _UnityOfWork.StateModified(vendedor);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        // GET: /Vendedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vendedor vendedor = db.Personas.Find(id);
            Vendedor vendedor = (Vendedor)_UnityOfWork.Personas.Get(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        // POST: /Vendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Vendedor vendedor = db.Personas.Find(id);
            Vendedor vendedor = (Vendedor)_UnityOfWork.Personas.Get(id);
            //db.Personas.Remove(vendedor);
            _UnityOfWork.Personas.Delete(vendedor);
            //db.SaveChanges();
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
