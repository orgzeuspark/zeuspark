using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class ProductVM
    {

        public ProductVM()
        {
            Properties = new Dictionary<string, string>();
            SkuInfoList = new List<SkuInfoVM>();
            Location = new LocationVM();
            Groups = new List<GroupVM>();
            Images = new List<ImageVM>();
            SaleInfo = new SaleInfoVM();
        }

        public int ProductID { get; set; }

        public List<ImageVM> Images { get; set; }

        public string ProductUnique { get; set; }

        public List<GroupVM> Groups { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public string MainImageUrl { get; set; }

        public Dictionary<string,string> Properties { get; set; }

        public List<SkuInfoVM> SkuInfoList { get; set; }

        public int BuyLimit { get; set; }

        public string DetailHtml { get; set; }

        public int Status { get; set; }

        public string DisplaySinglePrice { get; set; }

        public DeliveryInfoVM DeliveryInfo { get; set; }

        public LocationVM Location { get; set; }

        public SaleInfoVM SaleInfo { get; set; }

    }
}
