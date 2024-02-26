using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MediSync.Models
{
    [MetadataType(typeof(PhysicianMetadata))]
    public partial class Physician
    {
    }

    public class PhysicianMetadata
    {
        [Required]
        [Display(Name = "First Name")]
        public string physicianFirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string physicianLastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public System.DateTime physicianDOB { get; set; }
        [Required]
        [Display(Name = "Specialization")]
        public string physicianSpecialization { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string physicianGender { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string physicianContact { get; set; }
        [Required]
        [Display(Name = "Email ID")]
        public string physicianEmail { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string physicianAddress { get; set; }
        [Required]
        [Display(Name = "Summary")]
        public string physicianDescription { get; set; }
    }

}