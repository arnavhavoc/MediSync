using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediSync.Models;
using System.Net.Mail;

namespace MediSync.Controllers
{
    public class PatientCrudController : Controller
    {
        private MediSyncEntities db = new MediSyncEntities();

        // GET: PatientCrud
        public ActionResult Index()
        {
            if (Session["CurrentUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.Patients.ToList());
        }

        // GET: PatientCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: PatientCrud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientCrud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patientID,patientFirstName,patientLastName,patientDOB,patientGender,patientContact,patientEmail,patientAddress,patientMedicalHistory")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                string _ep = null;
                _ep = (from itr in db.Patients
                             where itr.patientEmail == patient.patientEmail
                             select itr.patientEmail).FirstOrDefault();

                if (_ep != null)
                {
                    ModelState.AddModelError("", "Email already registered.");
                    return View(patient);
                }

                db.Patients.Add(patient);
                db.SaveChanges();

                UserCred usr = new UserCred();
                usr.userName = patient.patientEmail;
                usr.userPassword = patient.patientLastName + patient.patientContact;
                usr.userRole = "Patient";
                usr.userReferenceToID = patient.patientID;
                db.UserCreds.Add(usr);
                db.SaveChanges();

                //Sending ID PASS To Email (SMTP)
                //string fromMail = "medisync.helpdesk@gmail.com";
                //string fromPassword = "pcxpxifqklgpnzom";
                //MailMessage message = new MailMessage();
                //message.From = new MailAddress(fromMail);
                //message.Subject = ($"Welcome To MediSync, {patient.patientFirstName}");
                //message.To.Add(new MailAddress($"{usr.userName}"));
                //message.Body = ($"<html>\r\n  <body>\r\n    <p style=\"text-align: center;\"><img src=\"https://i.ibb.co/qkZ2pJJ/Logo-Header.png\" alt=\"Logo-Header\" height=\"60px\" /></p>\r\n<p style=\"text-align: center;\"><strong>Welcome to our clinic family! Your journey to wellness begins here!</strong></p>\r\n<p style=\"text-align: left;\"><strong>Your Username: {usr.userName}</strong></p>\r\n<p style=\"text-align: left;\"><strong>Your Password: {usr.userPassword}</strong></p>\r\n<p style=\"text-align: left;\"><strong>Kindly update your password credentials by logging into our website, as this is an auto-generated password.</strong></p>\r\n  </body>\r\n</html>");
                //message.IsBodyHtml = true;
                //var smtpClient = new SmtpClient("smtp.gmail.com")
                //{
                //    Port = 587,
                //    Credentials = new NetworkCredential(fromMail, fromPassword),
                //    EnableSsl = true,
                //};
                //smtpClient.Send(message);
                //SMTP CODE ENDS HERE

                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: PatientCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: PatientCrud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patientID,patientFirstName,patientLastName,patientDOB,patientGender,patientContact,patientEmail,patientAddress,patientMedicalHistory")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: PatientCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: PatientCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
