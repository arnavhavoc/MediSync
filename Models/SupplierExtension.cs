using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediSync.Models
{
    [MetadataType(typeof(SupplierMetadata))]
    public partial class Supplier
    {
    }

    public class SupplierMetadata
    {
        [Required]
        [Display(Name = "Name")]
        public string supplierName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string supplierAddress { get; set; }
        [Required]
        [Display(Name = "Email ID")]
        public string supplierEmail { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string supplierContact { get; set; }
    }
}