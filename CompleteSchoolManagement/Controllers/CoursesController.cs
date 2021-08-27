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
    public class CoursesController : Controller
    {
        private readonly SchoolModelContainer db = new SchoolModelContainer();
        private readonly LogModel logModel = new LogModel();

        // GET: Courses
        public ActionResult Index(int id)
        {
            if (!logModel.IsLoggedTeacher)
                return RedirectToAction("Teacher", "Home");

            Teacher t = db.TeacherSet.Find(id);

            return View(t.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (!logModel.IsLoggedTeacher)
                return RedirectToAction("Teacher", "Home");

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

        // GET: Courses/Create
        public ActionResult Create()
        {
            if (!logModel.IsLoggedTeacher)
                return RedirectToAction("Teacher", "Home");

            ViewBag.TeacherId = new SelectList(db.TeacherSet, "Id", "UserName");
            ViewBag.tID = logModel.TeacherSession;
            return View();
        }

        // POST: Courses/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,StartDate,EndDate,Grade")] Courses courses)
        {
            ViewBag.tID = logModel.TeacherSession;
            if (ModelState.IsValid)
            {
                courses.TeacherId = Convert.ToInt32(logModel.TeacherSession);
                db.CoursesSet.Add(courses);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = ViewBag.tID });
            }

            return View(courses);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!logModel.IsLoggedTeacher)
                return RedirectToAction("Teacher", "Home");

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

        // POST: Courses/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,StartDate,EndDate,Grade")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { logModel.TeacherSession });
            }
            return View(courses);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!logModel.IsLoggedTeacher)
                return RedirectToAction("Teacher", "Home");

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

        // POST: Courses/Delete/5
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
