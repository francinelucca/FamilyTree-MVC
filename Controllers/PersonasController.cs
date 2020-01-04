using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyTree2.DATA;

namespace FamilyTree2.Controllers
{
    public class PersonasController : Controller
    {
        private FamilyTreeEntities db = new FamilyTreeEntities();

        // GET: Personas
        public ActionResult Index()
        {
            return View(db.Personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FirstName,LastName,BirthDay,Gender,createdAt,updatedAt")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.createdAt = DateTime.Now;
                db.Personas.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FirstName,LastName,BirthDay,Gender,createdAt,updatedAt")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                var currentPersona = db.Personas.FirstOrDefault(p => p.id == persona.id);
                if (currentPersona == null)
                    return HttpNotFound();
                currentPersona.Gender = persona.Gender;
                currentPersona.LastName = persona.LastName;
                currentPersona.FirstName = persona.FirstName;
                currentPersona.BirthDay = persona.BirthDay;
                currentPersona.updatedAt = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Relatives.RemoveRange(persona.Relatives);
            if(db.Relatives.Any(r => r.RelativeId == persona.id))
            {
                ViewBag.Message = "Cannot Remove Persona Because is Being Used as Other Personas Relative";
                return View(persona);
           //     return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Can't Remove Persona Because is Being Used as Other Personas' Relative");

            }
            db.Personas.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
