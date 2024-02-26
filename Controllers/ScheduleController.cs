using MediSync.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSync.Controllers
{
    public class ScheduleController : Controller
    {
        MediSyncEntities db = new MediSyncEntities();
        // GET: Schedule
        public ActionResult AddSchedule()
        {
            return View();
        }

        // GET Schedule
        

        //POST Schedule
        [HttpPost]
        public ActionResult AddSchedule([Bind(Include ="scheduleDateTime, scheduleAppointmentID, scheduleStatus")] Schedule schedule)
        {
            
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();

                Appointment appointment = (from itr in db.Appointments
                                          where schedule.scheduleAppointmentID == itr.appointmentID
                                          select itr).FirstOrDefault();
                appointment.appointmentStatus = "Approved";
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "AppointmentCrud");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        public ActionResult ViewScheduleAdmin()
        {
            var schedules = from patient in db.Patients
                            from physician in db.Physicians
                            from appointment in db.Appointments
                            where appointment.appointmentPatientID == patient.patientID && appointment.appointmentPhysicianID == physician.physicianID
                            from schedule in db.Schedules
                            where schedule.scheduleAppointmentID == appointment.appointmentID
                            select new ScheduleViewAdminModel
                            {
                                PatientName = patient.patientFirstName,
                                AppointmentDate = appointment.appointmentDateTime,
                                AppointmentReason = appointment.appointmentReason,
                                ScheduleDateTime = schedule.scheduleDateTime,
                                PhysicianName = physician.physicianFirstName
                            };
            return View(schedules.ToList());
        }

        //public ActionResult ViewSchedulePhysician()
        //{
        //    DateTime currentDate = DateTime.Now;
        //    CurrentUserModel currentUserModel = Session["CurrentUser"] as CurrentUserModel;
        //    var schedules = from patient in db.Patients
        //                    from physician in db.Physicians
        //                    from appointment in db.Appointments
        //                    from schedule in db.Schedules
        //                    where appointment.appointmentPatientID == patient.patientID && appointment.appointmentPhysicianID == currentUserModel.currentUserReferenceToId
        //                    select new ScheduleViewPhysicianModel
        //                    {
        //                        PatientName = patient.patientFirstName + patient.patientLastName,
        //                        PatientAge = (currentDate.Year - patient.patientDOB.Year),
        //                        PatientMedicalHistory = patient.patientMedicalHistory,
        //                        AppointmentReason = appointment.appointmentReason,
        //                        ScheduleStatus = schedule.scheduleStatus
        //                    };
        //    return View(schedules.ToList());

        //}

    }

}