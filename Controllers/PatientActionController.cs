using MediSync.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    public class PatientActionController : Controller
    {

        MediSyncEntities db = new MediSyncEntities();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patientID,patientFirstName,patientLastName,patientDOB,patientGender,patientContact,patientEmail,patientAddress,patientMedicalHistory")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;
                db.Entry(patient).State = EntityState.Modified;
                currentUserModel.currentUserFirstName = patient.patientFirstName;
                currentUserModel.currentUserLastName=patient.patientLastName;
                Session["CurrentUser"] = currentUserModel;
                db.SaveChanges();
                return RedirectToAction("Index", "Patient");
            }
            return View(patient);
        }

        //GET ChangePassword
        // GET: UserCredCrud/Edit/5
        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCred userCred = db.UserCreds.Find(id);
            if (userCred == null)
            {
                return HttpNotFound();
            }
            return View(userCred);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "userID,userName,userPassword,userRole,userReferenceToID")] UserCred userCred)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCred).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Patient");
            }
            return View(userCred);
        }

        //GET Book Appointment
        public ActionResult BookAppointment()
        {
            ViewBag.Physician=db.Physicians.ToList();

            return View();
        }

        //POST for Book Appointment
        [HttpPost]
        public ActionResult BookAppointment([Bind(Include = "appointmentID, appointmentPatientID, appointmentPhysicianID, appointmentDateTime, appointmentCriticality, appointmentReason, appointmentStatus")] Appointment appointment)
        {
            if(ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
            }
            ModelState.AddModelError("", "Appointment booked successfully!");
            return RedirectToAction("Index", "Patient");
        }

        public ActionResult ViewAppointments()
        {
            CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;
            var appointments = from itr in db.Appointments
                           where itr.appointmentPatientID == currentUserModel.currentUserReferenceToId
                           select itr;
            return View(appointments);
        }

        public ActionResult ViewSchedules()
        {
            CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;

            var schedules = from patient in db.Patients
                            where patient.patientID == currentUserModel.currentUserReferenceToId
                            from appointment in db.Appointments
                            where appointment.appointmentPatientID == patient.patientID
                            from schedule in db.Schedules
                            where schedule.scheduleAppointmentID == appointment.appointmentID
                            select new ScheduleViewModel
                            {
                                AppointmentDate = appointment.appointmentDateTime,
                                AppointmentReason = appointment.appointmentReason,
                                ScheduleDateTime = schedule.scheduleDateTime,
                                ScheduleID = schedule.scheduleID
                            };

            return View(schedules.ToList());
        }

        public ActionResult DownloadReport(int? ScheduleID)
        {
            CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;
            //Physician
            MediSyncEntities db = new MediSyncEntities();
            int currPhysician = (from sch in db.Schedules
                                from app in db.Appointments
                                where sch.scheduleAppointmentID == app.appointmentID
                                select app.appointmentPhysicianID).FirstOrDefault();
            var physicians = (from phy in db.Physicians
                             where phy.physicianID == currPhysician
                             select phy).FirstOrDefault();
            ViewBag.Physician = physicians;

            //Patient
            int currPatient = (from sch in db.Schedules
                                 from app in db.Appointments
                                 where sch.scheduleAppointmentID == app.appointmentID
                                 select app.appointmentPatientID).FirstOrDefault();
            var patients = (from pat in db.Patients
                            where pat.patientID == currPatient
                            select pat).FirstOrDefault();
            ViewBag.Patient = currentUserModel.currentUserFirstName.ToString();

            //ScheduleDate
            var scheduleDate = (from sch in db.Schedules
                                where sch.scheduleID==ScheduleID
                                select sch.scheduleDateTime).FirstOrDefault();
            ViewBag.ScheduleDate = scheduleDate.Date.ToString("dd-MMM-yyyy");

            //Advice
            string currAdv = (from adv in db.PhysicianAdvices
                             where adv.PhysicianAdviceScheduleID == ScheduleID
                             select adv.PhysicianAdviceText.ToString()).FirstOrDefault();
            ViewBag.Advice = currAdv;

            //Prescription
            var presc = from sch in db.Schedules
                        from adv in db.PhysicianAdvices
                        where sch.scheduleID == adv.PhysicianAdviceScheduleID
                        from pres in db.PhysicianPrescriptions
                        where pres.PhysicianPrescriptionAdviceID == adv.PhysicianAdviceID
                        from drg in db.Drugs
                        where pres.PhysicianPrescriptionDrugID == drg.drugID
                        select new DrugNameDoseModel
                        {
                            DrugName = drg.drugName,
                            DrugDose = pres.PhysicianPrescriptionDescription
                        };
            ViewBag.Prescription = presc.ToList();
            return View("DownloadReportFullScreen");
        }
    }
}