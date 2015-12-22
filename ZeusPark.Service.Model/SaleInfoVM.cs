using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class SaleInfoVM
    {
        public int SaleInfoID { get; set; }
        public int ProductID { get; set; }
        public string ProductUnique { get; set; }
        public int TotalSale { get; set; }
        public int OnSaleDay { get; set; }
        public string PayImageUrl { get; set; }
    }
}
