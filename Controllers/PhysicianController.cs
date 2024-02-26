using MediSync.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    [Authorize(Roles ="Physician")]
    public class PhysicianController : Controller
    {
        MediSyncEntities db = new MediSyncEntities();
        // GET: Physician
        public ActionResult Index()
        {

            DateTime currentDate = DateTime.Now;
            CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;

            var schedules = from appointment in db.Appointments
                            join patient in db.Patients on appointment.appointmentPatientID equals patient.patientID
                            join schedule in db.Schedules on appointment.appointmentID equals schedule.scheduleAppointmentID
                            where appointment.appointmentPhysicianID == currentUserModel.currentUserReferenceToId
                            select new ScheduleViewPhysicianModel
                            {
                                ScheduleID = schedule.scheduleID,
                                PatientName = patient.patientFirstName + " " + patient.patientLastName,
                                PatientAge = (currentDate.Year - patient.patientDOB.Year),
                                PatientMedicalHistory = patient.patientMedicalHistory,
                                AppointmentReason = appointment.appointmentReason,
                                ScheduleStatus = schedule.scheduleStatus
                            };

            ViewBag.Drugs=db.Drugs.ToList();

            return View(schedules.ToList());
        }

        public ActionResult ExaminePatient(int? scheduleID)
        {
            if (scheduleID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(scheduleID);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        public ActionResult AdvicePrescriptionForm(int? ScheduleID)
        {
            ViewBag.ScheduleID = ScheduleID;
            ViewBag.Drugs = db.Drugs.ToList();
            var model = new AdvicePrescriptionViewModel
            {
                Prescriptions = new List<PrescriptionViewModel> { new PrescriptionViewModel() }
            };


            return View(model);
        }

        [HttpPost]
        public ActionResult AdvicePrescriptionForm(AdvicePrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save advice to the database.
                var advice = new PhysicianAdvice
                {
                    PhysicianAdviceText = model.AdviceText,
                    PhysicianAdviceScheduleID = model.ScheduleID
                };

                db.PhysicianAdvices.Add(advice);
                db.SaveChanges();



                // Save prescriptions to the database.
                foreach (var prescription in model.Prescriptions)
                {
                    Drug drg = (from d in db.Drugs
                                where d.drugName == prescription.DrugName
                                select d).FirstOrDefault();
                    var physicianPrescription = new PhysicianPrescription
                    {
                        PhysicianPrescriptionAdviceID = advice.PhysicianAdviceID,
                        PhysicianPrescriptionDrugID = drg.drugID,
                        PhysicianPrescriptionDescription = prescription.Description
                    };

                    db.PhysicianPrescriptions.Add(physicianPrescription);
                }

                db.SaveChanges();

                Schedule sch = (from s in db.Schedules
                                where s.scheduleID == model.ScheduleID
                                select s).FirstOrDefault();
                sch.scheduleStatus = "Examined";
                db.Entry(sch).State = EntityState.Modified;
                db.SaveChanges();

                // Redirect to a success page or another action
                return RedirectToAction("Index", "Physician");
            }
;
            return RedirectToAction("Index", "Physician");
        }

        public ActionResult RequestDrug()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestDrug(DrugRequest drg)
        {
            db.DrugRequests.Add(drg);
            db.SaveChanges();
            return RedirectToAction("Index", "Physician");
        }


    }
}