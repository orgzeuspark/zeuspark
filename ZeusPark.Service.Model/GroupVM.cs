using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class GroupVM
    {
        public GroupVM()
        {

        }

        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public int ParentGroupID { get; set; }
    }
}
