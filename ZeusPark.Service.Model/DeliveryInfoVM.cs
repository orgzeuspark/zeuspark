using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class DeliveryInfoVM
    {
        public int DeliveryInfoID { get; set; }
        public int DeliveryType { get; set; }
        public string TemplateId { get; set; }
        public int ExpressNormalID { get; set; }
        public double ExpressNormalPrice { get; set; }
        public int ExpressFastID { get; set; }
        public double ExpressFastPrice { get; set; }
        public int ExpressEMSID { get; set; }
        public double ExpressEMSPrice { get; set; }
        public int Volume { get; set; }
        public int Weight { get; set; }
    }
}
