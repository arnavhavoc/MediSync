//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediSync.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Physician
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Physician()
        {
            this.Appointments = new HashSet<Appointment>();
            this.DrugRequests = new HashSet<DrugRequest>();
        }
    
        public int physicianID { get; set; }
        public string physicianFirstName { get; set; }
        public string physicianLastName { get; set; }
        public System.DateTime physicianDOB { get; set; }
        public string physicianSpecialization { get; set; }
        public string physicianGender { get; set; }
        public string physicianContact { get; set; }
        public string physicianEmail { get; set; }
        public string physicianAddress { get; set; }
        public string physicianDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DrugRequest> DrugRequests { get; set; }
    }
}