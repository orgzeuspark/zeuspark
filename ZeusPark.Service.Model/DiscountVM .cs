using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class DiscountVM
    {
        public DiscountVM()
        {

        }

        public int DiscountID { get; set; }

        public string   CodeID { get; set; }

        public string Title { get; set; }

        public int CodeType { get; set; }

        public int Quantity { get; set; }

        public int TotalQuantity { get; set; }

        public decimal DiscountNum { get; set; }

        public decimal ConditionNum { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }

        public bool IsValid { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
