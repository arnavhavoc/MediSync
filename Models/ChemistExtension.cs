using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediSync.Models
{
    [MetadataType(typeof(ChemistMetadata))]
    public partial class Chemist
    {
    }

    public class ChemistMetadata
    {
        [Required]
        [Display(Name = "Name")]
        public string chemistName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string chemistAddress { get; set; }
        [Required]
        [Display(Name = "Email ID")]
        public string chemistEmail { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string chemistContact { get; set; }
        [Required]
        [Display(Name = "Summary")]
        public string chemistDescription { get; set; }
    }
}