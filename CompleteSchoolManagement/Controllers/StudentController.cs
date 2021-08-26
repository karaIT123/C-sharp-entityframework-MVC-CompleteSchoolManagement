using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompleteSchoolManagement.Models;

namespace CompleteSchoolManagement.Controllers
{
    public class StudentController : Controller
    {
        SchoolModelContainer context = new SchoolModelContainer();
        EmailModel email = new EmailModel();

        int used = new int();
        
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            Student student = context.StudentSet.Find(id);
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                
                Student filledStudent = FillStudentForLogin(student);
                string msg = generateEmail(filledStudent);
                email.SendEmail(filledStudent.Email,"Information de connection",msg);
                context.StudentSet.Add(filledStudent);
                context.SaveChanges();

                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        Student FillStudentForLogin(Student student)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string password = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            string username = student.LastName + student.FirstName[0] + Convert.ToString(new Random().Next(0000,9999));
            student.isFirstLogin = "true";
            student.UserName = username;
            student.Password = password;
            return student;
        }

        String generateEmail(Student student)
        {
            string msg = "<html lang='en'>" +
                "<head>" +
                "<!--Required meta tags-->" +
                "<meta charset = 'utf-8'>" +
                "<meta name = 'viewport' content = 'width=device-width, initial-scale=1'>" +
                "<!--Bootstrap CSS-->" +
                "<link href = 'https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/css/bootstrap.min.css' rel = 'stylesheet' integrity = 'sha384-KyZXEAg3QhqLMpG8r+8fhAXLRk2vvoC2f3B09zVXn8CA5QIVfZOJ3BCsw2P0p/We' crossorigin = 'anonymous'>" +
                "<title> Mail </title>" +
                "</head>" +
                "<body>" +
                "<div class='container'>" +
                "<div class='alert alert-primary' role='alert'>" +
                "Informations de connection" +
                "</div>" +
                "<div class='d-flex justify-content-center'>" +
                "<h3>" +
                "Veuiller noter que vous devez imperativement changer votre mot de passe lors de votre premiere connection" +
                "</h3>" +
                "</div>" +
                "<div class='card text-dark bg-info mb-3 mt-4' style='max-width: 100%'>" +
                "<div class='card-header'>Garger vos information secret</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title'>Nom d'utilisateur / Mot de passe</h5>" +
                "<p class='card-text'>Nom d'utilisateur: " +
                "<span class='fw-bold'>"+student.UserName+"</span>" +
                "</p>" +
                "<p class='card-text'>Mot de passe: " +
                "<span class='fw-bold'>"+student.Password+"</span>" +
                "</p>" +
                "</div" +
                "</div>" +
                "</div>" +
                "<script src = 'https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js' integrity='sha384-U1DAWAznBHeqEIlVSCgzq+c9gqGAJn5c/t99JyeKa9xxaYpSvHU5awsuZVVFIhvj' crossorigin='anonymous'></script>" +
                "</body>" +
                "</html>";
            return msg;
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                Student s = context.StudentSet.Find(id);

                s.FirstName = String.IsNullOrEmpty(student.FirstName) ? s.FirstName : student.FirstName;
                s.LastName = String.IsNullOrEmpty(student.LastName) ? s.LastName : student.LastName;
                s.Email = String.IsNullOrEmpty(student.Email) ? s.FirstName : student.Email;
                s.UserName = String.IsNullOrEmpty(student.UserName) ? s.UserName : student.UserName;
                s.Password = String.IsNullOrEmpty(student.Password) ? s.Password : student.Password;
                s.Grade = String.IsNullOrEmpty(student.Grade) ? s.Grade : student.Grade;




                context.SaveChanges();
                

                return RedirectToAction("Details/" + id, "Student");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SubscribeToCourses(int id)
        {
            ViewBag.sId = id;

            Student s = context.StudentSet.Find(id);
            string grade = s.Grade;

            List<Courses> lc = context.CoursesSet.Where(x => x.Grade == grade).ToList();
            List<Courses> slc = s.Courses.ToList();

            for (int i = 0; i < lc.Count; i++)
            {
                foreach (Courses item2 in slc)
                {
                    if (lc[i] == item2)
                    {
                        lc.Remove(lc[i]);
                    }
                }
            }

            ViewBag.notCourses = false;
            if (lc.Count == 0)
            {
                ViewBag.notCourses = true;
            }

            return View(lc);
        }

        
        public ActionResult Material(int id)
        {
            int rId = Convert.ToInt32(Session["user"]);
            ViewBag.rId = rId;
            Material m = context.CoursesSet.Find(id).Material.FirstOrDefault();
            return View(m);
        }
        public ActionResult MyCourses(int id)
        {
            ViewBag.sId = id;
            List<Courses> lc = context.StudentSet.Find(id).Courses.ToList();
            return View(lc);
        }

        public ActionResult AddCourses(int studentId, int CoursesId)
        {
            Student s = context.StudentSet.Find(studentId);
            Courses c = context.CoursesSet.Find(CoursesId);

            s.Courses.Add(c);
            context.SaveChanges();
            return RedirectToAction("SubscribeToCourses/"+studentId);
        }

        public ActionResult RemoveCourses(int studentId, int CoursesId)
        {
            Student s = context.StudentSet.Find(studentId);
            Courses c = context.CoursesSet.Find(CoursesId);

            s.Courses.Remove(c);
            context.SaveChanges();
            return RedirectToAction("SubscribeToCourses/" + studentId);
        }

        


    }
}
