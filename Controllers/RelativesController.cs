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
    public class RelativesController : Controller
    {
        private FamilyTreeEntities db = new FamilyTreeEntities();

        // GET: Relatives
        public ActionResult Index()
        {
            var relatives = db.Relatives.Include(r => r.Persona).Include(r => r.Relationship).Include(r => r.RelatedTo);
            return View(relatives.ToList());
        }

        // GET: Relatives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relative relative = db.Relatives.Find(id);
            if (relative == null)
            {
                return HttpNotFound();
            }
            return View(relative);
        }

        // GET: Relatives/Create
        public ActionResult Create(int? personaId)
        {
            ViewBag.PersonaId = new SelectList (new List<Persona>() { db.Personas.Find(personaId) }, "id", "fullName");
            ViewBag.RelationshipId = new SelectList(db.Relationships, "id", "Relationship1");
            ViewBag.RelativeId = new SelectList(db.Personas.Where(p => p.id != personaId), "id", "fullName");
            return View();
        }

        // POST: Relatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,PersonaId,RelationshipId,createdAt,updatedAt,RelativeId")] Relative relative)
        {
            if (ModelState.IsValid)
            {
                relative.createdAt = DateTime.Now;
                db.Relatives.Add(relative);
                db.SaveChanges();
                return RedirectToAction("Details", "Personas", new { id = relative.PersonaId });
            }

            ViewBag.PersonaId = new SelectList(db.Personas, "id", "fullName", relative.PersonaId);
            ViewBag.RelationshipId = new SelectList(db.Relationships, "id", "Relationship1", relative.RelationshipId);
            ViewBag.RelativeId = new SelectList(db.Personas, "id", "fullName", relative.RelativeId);
            return View(relative);
        }

        // GET: Relatives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relative relative = db.Relatives.Find(id);
            if (relative == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaId = new SelectList(new List<Persona>() { db.Personas.Find(relative.PersonaId) }, "id", "fullName", relative.PersonaId);
            ViewBag.RelationshipId = new SelectList(db.Relationships, "id", "Relationship1", relative.RelationshipId);
            ViewBag.RelativeId = new SelectList(db.Personas, "id", "fullName", relative.RelativeId);
            return View(relative);
        }

        // POST: Relatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,PersonaId,RelationshipId,createdAt,updatedAt,RelativeId")] Relative relative)
        {
            if (ModelState.IsValid)
            {
                var currentRelative = db.Relatives.FirstOrDefault(p => p.id == relative.id);
                if (currentRelative == null)
                    return HttpNotFound();
                currentRelative.RelationshipId = relative.RelationshipId;
                currentRelative.RelativeId = relative.RelativeId;
                currentRelative.updatedAt = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", "Personas", new { id = currentRelative.PersonaId });
            }
            ViewBag.PersonaId = new SelectList(new List<Persona>() { db.Personas.Find(relative.PersonaId) }, "id", "fullName", relative.PersonaId);
            ViewBag.RelationshipId = new SelectList(db.Relationships, "id", "Relationship1", relative.RelationshipId);
            ViewBag.RelativeId = new SelectList(db.Personas, "id", "fullName", relative.RelativeId);
            return View(relative);
        }

        // GET: Relatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relative relative = db.Relatives.Find(id);
            if (relative == null)
            {
                return HttpNotFound();
            }
            return View(relative);
        }

        // POST: Relatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relative relative = db.Relatives.Find(id);
            int personaId = relative.PersonaId;
            db.Relatives.Remove(relative);
            db.SaveChanges();
            return RedirectToAction("Details", "Personas", new { id = personaId });
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
