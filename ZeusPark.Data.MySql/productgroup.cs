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
    
    public partial class productgroup
    {
        public int ProductGroupID { get; set; }
        public int ProductID { get; set; }
        public int GroupID { get; set; }
        public string ProductUnique { get; set; }
    
        public virtual product product { get; set; }
        public virtual groupvalue groupvalue { get; set; }
    }
}
