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
    
    public partial class DrugRequest
    {
        public int drugRequestID { get; set; }
        public int drugRequestPhysicianID { get; set; }
        public string drugRequestInfo { get; set; }
        public System.DateTime drugRequestDate { get; set; }
        public string drugRequestStatus { get; set; }
    
        public virtual Physician Physician { get; set; }
    }
}