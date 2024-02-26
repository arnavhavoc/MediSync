using MediSync.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    [Authorize(Roles ="Patient")]
    public class PatientController : Controller
    {
        MediSyncEntities db = new MediSyncEntities();
        // GET: Patient
        public ActionResult Index()
        {
            ViewBag.Physician = db.Physicians.ToList();

            CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;

            var appointments = from itr in db.Appointments
                               where itr.appointmentPatientID == currentUserModel.currentUserReferenceToId
                               select itr;

            return View(appointments);
        }

        
    }

}