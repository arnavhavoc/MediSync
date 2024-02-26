using MediSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            MediSyncEntities db = new MediSyncEntities();
            if (Session["CurrentUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var pendApp = from itr in db.Appointments
                              where itr.appointmentStatus == "Pending"
                              select itr;
                ViewBag.pendingAppointments = pendApp.ToList().Count();

                var sechApp = from itr in db.Appointments
                              where itr.appointmentStatus== "Approved"
                              select itr;
                ViewBag.secheduledAppointment = sechApp.Count();  
                return View();
            }
            
        }
        
    }
}