using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace CompleteSchoolManagement.Models
{
    public class EmailModel
    {
        public EmailModel( )
        {}

        public void SendEmail(string receiver, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress("karacoulibaly200@gmail.com", "Karamoko Coukibaly");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "Mariam98";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                    
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                
            }
            catch (Exception e)
            {
                
            }
        }

        public void SendNoTWorking(string receiver, string subject, string message)
        {
            try
            {
                WebMail.SmtpServer = "stmp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = true;
                WebMail.UserName = "karacoulibaly200@gmail.com";
                WebMail.Password = "Mariam98";

                WebMail.Send(to: receiver, subject: subject, body: message, isBodyHtml: true);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void SendNotWorking(string receiver, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(receiver);
                mail.From = new MailAddress("karacoulibaly200@gmail.com");
                mail.Subject = subject;
                string Body = message;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("karacoulibaly200@gmail.com", "Mariam98"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}