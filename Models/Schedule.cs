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
    
    public partial class Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schedule()
        {
            this.PhysicianAdvices = new HashSet<PhysicianAdvice>();
        }
    
        public int scheduleID { get; set; }
        public System.DateTime scheduleDateTime { get; set; }
        public int scheduleAppointmentID { get; set; }
        public string scheduleStatus { get; set; }
    
        public virtual Appointment Appointment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhysicianAdvice> PhysicianAdvices { get; set; }
    }
}
