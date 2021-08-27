using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompleteSchoolManagement.Models
{
    public class LogModel : System.Web.UI.Page
    {
        public int StudentSession
        {
            get
            {
                //Session["dfrfd"] = 1;
                return HttpContext.Current.Session["Student"] == null ?
                    0 : (int)HttpContext.Current.Session["Student"];
            }
            set
            {
                HttpContext.Current.Session["Student"] = value;
            }
        }

        public int TeacherSession
        {
            get
            {
                //Session["dfrfd"] = 1;
                return HttpContext.Current.Session["Teacher"] == null ?
                    0 : (int)HttpContext.Current.Session["Teacher"];
            }
            set
            {
                HttpContext.Current.Session["Teacher"] = value;
            }
        }


        public bool IsLoggedStudent
        {
            get
            {
                return StudentSession > 0 ? true : false;
            }
            
        }

        public bool IsLoggedTeacher
        {
            get
            {
                return TeacherSession > 0 ? true : false;
            }

        }

        public void LogOut()
        {
            StudentSession = 0;

        }

    }
   
}