//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeusPark.Data.MySql
{
    using System;
    using System.Collections.Generic;
    
    public partial class deliveryinfo
    {
        public deliveryinfo()
        {
            this.products = new HashSet<product>();
        }
    
        public int DeliveryInfoID { get; set; }
        public Nullable<int> DeliveryType { get; set; }
        public string TemplateId { get; set; }
        public Nullable<int> ExpressNormalID { get; set; }
        public Nullable<double> ExpressNormalPrice { get; set; }
        public Nullable<int> ExpressFastID { get; set; }
        public Nullable<double> ExpressFastPrice { get; set; }
        public Nullable<int> ExpressEMSID { get; set; }
        public Nullable<double> ExpressEMSPrice { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> Weight { get; set; }
    
        public virtual ICollection<product> products { get; set; }
    }
}
