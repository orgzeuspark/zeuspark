using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class ProductGroupVM
    {
        public ProductGroupVM()
        {
            Products = new List<ProductVM>();
        }

        public string GroupName { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}
