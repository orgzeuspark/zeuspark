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
    
    public partial class groupvalue
    {
        public groupvalue()
        {
            this.productgroups = new HashSet<productgroup>();
        }
    
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> ParentGroupID { get; set; }
        public bool Deleted { get; set; }
    
        public virtual ICollection<productgroup> productgroups { get; set; }
    }
}
