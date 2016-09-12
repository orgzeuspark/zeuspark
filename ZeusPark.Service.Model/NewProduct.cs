using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class NewProduct
    {
        public string prodName { get; set; }

        public string simpleDesc { get; set; }

        public double price { get; set; }

        public int groupid { get; set; }

        public int quantity { get; set; }

        public string detailDesc { get; set; }

        public List<string> mainfileids { get; set; }

        public List<string> otherfileids { get; set; }

        public double orgprice { get; set; }

        public string keyword { get; set; }

        public int userid { get; set; }

        public List<PriceDetail> priceDetail { get; set; }
    }

    public class PriceDetail
    {
        public double price { get; set; }

        public double orgprice { get; set; }

        public int quantity { get; set; }

        public string size { get; set; }

        public string color { get; set; }

    }

}
