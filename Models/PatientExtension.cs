using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MediSync.Models
{
    [MetadataType(typeof(PatientMetadata))]
    public partial class Patient
    {
    }

    public class PatientMetadata
    {
        [Required]
        [Display(Name ="First Name")]
        public string patientFirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string patientLastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public System.DateTime patientDOB { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string patientGender { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string patientContact { get; set; }
        [Required]
        [Display(Name = "Email ID")]
        public string patientEmail { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string patientAddress { get; set; }
        [Required]
        [Display(Name = "Medical Summary")]
        public string patientMedicalHistory { get; set; }
    }
}