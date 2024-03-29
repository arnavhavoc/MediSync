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
    
    public partial class Drug
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Drug()
        {
            this.PhysicianPrescriptions = new HashSet<PhysicianPrescription>();
            this.PurchaseProductLines = new HashSet<PurchaseProductLine>();
        }
    
        public int drugID { get; set; }
        public string drugName { get; set; }
        public System.DateTime drugExpiry { get; set; }
        public string drugDescription { get; set; }
        public string drugDosage { get; set; }
        public decimal drugPrice { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhysicianPrescription> PhysicianPrescriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseProductLine> PurchaseProductLines { get; set; }
    }
}
