using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class SkuInfoVM
    {
        public int InfoID { get; set; }

        public string IconUrl { get; set; }

        public double OrgPrice { get; set; }

        public double Price { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public string skuId { get; set; }
    }
}
