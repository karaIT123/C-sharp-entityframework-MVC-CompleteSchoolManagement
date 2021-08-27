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
    public class Courses1Controller : Controller
    {
        private SchoolModelContainer db = new SchoolModelContainer();

        // GET: Courses1
        public ActionResult Index()
        {
            var coursesSet = db.CoursesSet.Include(c => c.Teacher);
            return View(coursesSet.ToList());
        }

        // GET: Courses1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.CoursesSet.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Courses1/Create
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "UserName");
            return View();
        }

        // POST: Courses1/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,StartDate,EndDate,Grade,TeacherId")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.CoursesSet.Add(courses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "UserName", courses.TeacherId);
            return View(courses);
        }

        // GET: Courses1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.CoursesSet.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "UserName", courses.TeacherId);
            return View(courses);
        }

        // POST: Courses1/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,StartDate,EndDate,Grade,TeacherId")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "UserName", courses.TeacherId);
            return View(courses);
        }

        // GET: Courses1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.CoursesSet.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.CoursesSet.Find(id);
            db.CoursesSet.Remove(courses);
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
