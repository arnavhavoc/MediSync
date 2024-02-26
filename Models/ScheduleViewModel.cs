using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediSync.Models
{
    public class ScheduleViewModel
    {
        public DateTime AppointmentDate { get; set; }
        public string AppointmentReason { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public int ScheduleID { get; set; }
    }

    public class ScheduleViewAdminModel
    {
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentReason { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public string PhysicianName { get; set; }
    }

    public class ScheduleViewPhysicianModel
    {
        public int ScheduleID { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientMedicalHistory { get; set; }
        public string AppointmentReason { get; set; }
        public string ScheduleStatus { get; set; }
    }

    public class AdvicePrescriptionModel
    {
        public int scheduleID { get; set; }
        public string drugName { get; set; }
        public string PhysicianPrescriptionDescription { get; set; }
    }

    // PrescriptionViewModel.cs
    public class PrescriptionViewModel
    {
        public string DrugName { get; set; }
        public string Description { get; set; }
    }

    // AdvicePrescriptionViewModel.cs
    public class AdvicePrescriptionViewModel
    {
        public int ScheduleID { get; set; }
        public string AdviceText { get; set; }
        public List<PrescriptionViewModel> Prescriptions { get; set; }
    }

    public class POLineModel
    {
        public int purchaseOrderDrugID { get; set; }
        public int purchaseOrderQuantity { get; set; }
        public string purchaseOrderNote { get; set; }
    }

    public class POHeaderModel
    {
        public int purchaseOrderSupplierID { get; set; }
        public List<POLineModel> POLines {  get; set; }
    }

    public class DrugNameDoseModel
    {
        public string DrugName { get; set; }
        public string DrugDose { get; set; }
    }
}