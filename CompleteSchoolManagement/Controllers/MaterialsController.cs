using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompleteSchoolManagement.Models;

namespace CompleteSchoolManagement.Controllers
{
    public class MaterialsController : Controller
    {
        private SchoolModelContainer db = new SchoolModelContainer();

        // GET: Materials
        public ActionResult Index()
        {
            var materialSet = db.MaterialSet.Include(m => m.Courses);
            return View(materialSet.ToList());
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.MaterialSet.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // GET: Materials/Create
        public ActionResult Create()
        {
            ViewBag.CoursesId = new SelectList(db.CoursesSet, "Id", "Name");
            return View();
        }

        // POST: Materials/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Note,Movie,Laboratory,Exercise,Solution,CoursesId")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.MaterialSet.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoursesId = new SelectList(db.CoursesSet, "Id", "Name", material.CoursesId);
            return View(material);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.MaterialSet.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoursesId = new SelectList(db.CoursesSet, "Id", "Name", material.CoursesId);
            return View(material);
        }

        // POST: Materials/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Note,Movie,Laboratory,Exercise,Solution,CoursesId")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoursesId = new SelectList(db.CoursesSet, "Id", "Name", material.CoursesId);
            return View(material);
        }

        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.MaterialSet.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.MaterialSet.Find(id);
            db.MaterialSet.Remove(material);
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
