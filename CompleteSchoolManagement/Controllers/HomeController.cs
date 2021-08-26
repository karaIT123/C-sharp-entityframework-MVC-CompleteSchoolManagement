using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompleteSchoolManagement.Models;

namespace CompleteSchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        string adminUsername = "root";
        string adminPassword = "root";
        SchoolModelContainer context = new SchoolModelContainer();
        public ActionResult Index()
        {
            ViewBag.error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Student student)
        {
            //int count = context.StudentSet.Count();

            //var studentFind = context.StudentSet.Where(x => x.Email == "jane@gmail.com" && x.Password == "Jane5555").Count();
            int studentCount = context.StudentSet.Where(x => x.UserName == student.UserName && x.Password == student.Password).Count();
            if (studentCount > 0)
            {
                Student s = context.StudentSet.Where(x => x.UserName == student.UserName && x.Password == student.Password).FirstOrDefault();

                if (s.isFirstLogin == "true")
                {
                    return RedirectToAction("ChangePassword/" + s.Id, "Home");
                }
                else
                {
                    Session["user"] = s.Id;
                    return RedirectToAction("Details/" + s.Id, "Student");
                }
            }
            else
            {
                Url.Action("Index", "Home");

                ViewBag.error = "<div class='alert alert-danger' role='alert'>" +
                    "Erreur, reessayer encore </div>";
            }
            return View();
        }

        public ActionResult Teacher()
        {
            //ViewBag.error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Teacher(Teacher teacher)
        {
            //int count = context.StudentSet.Count();
            if(teacher.UserName == adminUsername && teacher.Password == adminPassword)
            {
                return RedirectToAction("Index", "Teachers");
            }

            //var studentFind = context.StudentSet.Where(x => x.Email == "jane@gmail.com" && x.Password == "Jane5555").Count();
            int teacherCount = context.TeacherSet.Where(x => x.UserName == teacher.UserName && x.Password == teacher.Password).Count();
            if (teacherCount > 0)
            {
                Teacher t = context.TeacherSet.Where(x => x.UserName == teacher.UserName && x.Password == teacher.Password).FirstOrDefault();
                return RedirectToAction("Index", "Courses");
            }
            else
            {
                Url.Action("Teacher", "Home");

                ViewBag.error = "<div class='alert alert-danger' role='alert'>" +
                    "Erreur, reessayer encore </div>";
            }
            return View();
        }

        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(int id, Student student)
        {
            Student s = context.StudentSet.Find(id);
            s.UserName = student.UserName;
            s.Password = student.Password;
            s.isFirstLogin = "false";

            context.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        public ActionResult Home()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}