using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediSync.Models;

namespace MediSync.Controllers
{
    public class AppointmentCrudController : Controller
    {
        private MediSyncEntities db = new MediSyncEntities();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Patient).Include(a => a.Physician);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.appointmentPatientID = new SelectList(db.Patients, "patientID", "patientFirstName");
            ViewBag.appointmentPhysicianID = new SelectList(db.Physicians, "physicianID", "physicianFirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "appointmentID,appointmentPatientID,appointmentPhysicianID,appointmentDateTime,appointmentCriticality,appointmentReason,appointmentStatus")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.appointmentPatientID = new SelectList(db.Patients, "patientID", "patientFirstName", appointment.appointmentPatientID);
            ViewBag.appointmentPhysicianID = new SelectList(db.Physicians, "physicianID", "physicianFirstName", appointment.appointmentPhysicianID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.appointmentPatientID = new SelectList(db.Patients, "patientID", "patientFirstName", appointment.appointmentPatientID);
            ViewBag.appointmentPhysicianID = new SelectList(db.Physicians, "physicianID", "physicianFirstName", appointment.appointmentPhysicianID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "appointmentID,appointmentPatientID,appointmentPhysicianID,appointmentDateTime,appointmentCriticality,appointmentReason,appointmentStatus")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.appointmentPatientID = new SelectList(db.Patients, "patientID", "patientFirstName", appointment.appointmentPatientID);
            ViewBag.appointmentPhysicianID = new SelectList(db.Physicians, "physicianID", "physicianFirstName", appointment.appointmentPhysicianID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
