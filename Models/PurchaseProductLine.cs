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
    
    public partial class PurchaseProductLine
    {
        public int purchaseOrderID { get; set; }
        public int purchaseOrderDrugID { get; set; }
        public int purchaseOrderSerialNo { get; set; }
        public int purchaseOrderQuantity { get; set; }
        public string purchaseOrderNote { get; set; }
    
        public virtual Drug Drug { get; set; }
        public virtual PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}
