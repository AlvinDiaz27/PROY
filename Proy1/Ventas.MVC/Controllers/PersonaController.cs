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
    public class PersonaController : Controller
    {
       // private Proy1DbContext db = new Proy1DbContext();
        private readonly IUnityOfWork _UnityOfWork;

        // GET: /Persona/
         public PersonaController()
        {

        }

        public PersonaController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
        public ActionResult Index()
        {
            //return View(db.Personas.ToList());
            return View(_UnityOfWork.Personas.GetAll());
        }

        // GET: /Persona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Persona persona = db.Personas.Find(id);
            Persona persona = _UnityOfWork.Personas.Get(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: /Persona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Persona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PersonaId,Nombre,ApePaterno,ApeMaterno,Direccion,Telefono,Correo")] Persona persona)
        {
            if (ModelState.IsValid)
            {
               // db.Personas.Add(persona);
                _UnityOfWork.Personas.Add(persona);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: /Persona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Persona persona = db.Personas.Find(id);
            Persona persona = _UnityOfWork.Personas.Get(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: /Persona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PersonaId,Nombre,ApePaterno,ApeMaterno,Direccion,Telefono,Correo")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(persona).State = EntityState.Modified;
                _UnityOfWork.StateModified(persona);
                //db.SaveChanges();
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: /Persona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Persona persona = db.Personas.Find(id);
            Persona persona = _UnityOfWork.Personas.Get(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: /Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Persona persona = db.Personas.Find(id);
            Persona persona = _UnityOfWork.Personas.Get(id);
            //db.Personas.Remove(persona);
            _UnityOfWork.Personas.Delete(persona);
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
