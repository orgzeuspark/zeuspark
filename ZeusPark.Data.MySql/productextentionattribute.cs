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
    
    public partial class productextentionattribute
    {
        public productextentionattribute()
        {
            this.products = new HashSet<product>();
        }
    
        public int ExtentionAttributeID { get; set; }
        public bool IsHasReceipt { get; set; }
        public bool IsPostFree { get; set; }
        public bool IsSupportReplace { get; set; }
        public bool IsUnderGuaranty { get; set; }
        public Nullable<int> LocationID { get; set; }
    
        public virtual ICollection<product> products { get; set; }
        public virtual location location { get; set; }
    }
}
