using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class OrderVM
    {
        public OrderVM()
        {

        }

        public int OrderID { get; set; }

        public string OrderUnique { get; set; }

        public int OrderUserId { get; set; }

        public decimal OrderAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public string DiscountCode { get; set; }

        public string OrderDetail { get; set; }

        public string ProdIds { get; set; }

        public string OrderImg { get; set; }

        public int Status { get; set; }

        public string Contactor { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

    }
}
