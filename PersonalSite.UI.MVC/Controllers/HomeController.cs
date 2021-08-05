using PersonalSite.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PersonalSite.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string message = "Email: " + (cvm.Email) + "<br />Name: " + (cvm.Name) + "<br />Subject: " + (cvm.Subject) + "<br />" + (cvm.Message);

            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, message);

            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            mm.ReplyToList.Add(cvm.Email);


            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be processed at this time. Please try again later. Error Message:</br> {ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }
}